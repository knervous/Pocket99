using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

[InitializeOnLoad]
public class NoesisUpdater: EditorWindow
{
    static NoesisUpdater()
    {
        EditorApplication.update += CheckVersion;
    }

    static void CheckVersion()
    {
        EditorApplication.update -= CheckVersion;

        string localVersion = NoesisVersion.GetCached();
        string version = NoesisVersion.Get();

        if (localVersion != version && version != "0.0.0")
        {
            if (NoesisVersion.RestartNeeded())
            {
                Debug.LogWarning("Please restart Unity to reload NoesisGUI native plugin!");
                return;
            }

            var window = (NoesisUpdater)ScriptableObject.CreateInstance(typeof(NoesisUpdater));
            window.titleContent = new GUIContent("NoesisGUI");
            window.position = new Rect(Screen.currentResolution.width / 2 - 250, Screen.currentResolution.height / 2 - 22, 500, 55);
            window.minSize = new Vector2(500, 55);
            window.maxSize = new Vector2(500, 55);
            window.localVersion_ = localVersion;
            window.version_ = version;

            if (localVersion != "")
            {
                window.label_ = "Upgrading NoesisGUI " + localVersion + " -> " + version;
            }
            else
            {
                window.label_ = "Installing NoesisGUI " +  version;
            }

            window.ShowUtility();
        }
    }

    private string localVersion_;
    private string version_;
    private string label_;
    private string state_;
    private float progress_ = 0.0f;
    private IEnumerator updater_;

    void OnEnable()
    {
        updater_ = UpdateVersion();
    }

    void OnGUI()
    {
        GUI.Label(new Rect (5, 5, 420, 20), label_);
        EditorGUI.ProgressBar(new Rect(5, 25, 490, 20), progress_, state_);
    }

    void OnInspectorUpdate()
    {
        if (updater_.MoveNext())
        {
            Repaint();
        }
        else
        {
            Close();
        }
    }

    private IEnumerator UpdateVersion()
    {
        GoogleAnalyticsHelper.LogEvent("Install", version_, 0);
        progress_ = 0.10f;

        state_ = "Upgrading project";
        yield return null;
        Upgrade(localVersion_);
        progress_ = 0.20f;

        state_ = "Updating version";
        yield return null;
        NoesisVersion.SetCached(version_);
        progress_ = 0.35f;

        state_ = "Creating default settings";
        yield return null;
        NoesisSettings.Get();
        progress_ = 0.40f;

        state_ = "Extracting documentation...\n";
        yield return null;
        ExtractDocumentation();
        progress_ = 0.90f;

        state_ = "Opening Welcome Window...\n";
        yield return null;
        EditorWindow.GetWindow(typeof(NoesisWelcome), true, "Welcome to NoesisGUI!");
        progress_ = 1.0f;

        Debug.Log("NoesisGUI v" + version_ + " successfully installed");
    }

    private static string NormalizeVersion(string version)
    {
        string pattern = @"^(\d+).(\d+).(\d+)((a|b|rc|f)(\d*))?$";
        var match = Regex.Match(version.ToLower(), pattern);

        string normalized = "";

        if (match.Success)
        {
            normalized += match.Groups[1].Value.PadLeft(2, '0');
            normalized += ".";
            normalized += match.Groups[2].Value.PadLeft(2, '0');
            normalized += ".";
            normalized += match.Groups[3].Value.PadLeft(2, '0');

            if (match.Groups[4].Length > 0)
            {
                if (match.Groups[5].Value == "a")
                {
                    normalized += ".0.";
                }
                else if (match.Groups[5].Value == "b")
                {
                    normalized += ".1.";
                }
                else if (match.Groups[5].Value == "rc")
                {
                    normalized += ".2.";
                }
                else if (match.Groups[5].Value == "f")
                {
                    normalized += ".3.";
                }

                normalized += match.Groups[6].Value.PadLeft(2, '0');
            }
            else
            {
                normalized += ".3.00";
            }
        }
        else
        {
            Debug.LogError("Unexpected version format " + version);
        }

        return normalized;
    }

    private static bool PatchNeeded(string from, string to)
    {
        if (from.Length == 0)
        {
            return false;
        }
        else
        {
            return String.Compare(NormalizeVersion(from), NormalizeVersion(to)) < 0;
        }
    }

    private static void Upgrade(string version)
    {
        if (PatchNeeded(version, "1.3.0a1"))
        {
            Debug.LogError("Upgrading from '" + version + "' not supported. Please install in a clean project");
        }

        /*if (PatchNeeded(version, "1.2.6f1"))
        {
            Upgrade_1_2_6f1();
        }*/
    }

    private static void EnsureFolder(string path)
    {
        if (!AssetDatabase.IsValidFolder(path))
        {
            string parentFolder = System.IO.Path.GetDirectoryName(path);
            string newFolder = System.IO.Path.GetFileName(path);

            AssetDatabase.CreateFolder(parentFolder, newFolder);
        }
    }

    private static string TarLocation = "NoesisGUI/Doc.tar";

    private static void ExtractDocumentation()
    {
        string tarPath = Path.Combine(Application.dataPath, TarLocation);

        if (File.Exists(tarPath))
        {
            string destPath = Application.dataPath + "/../NoesisDoc";
            byte[] buffer = new byte[512];

            try
            {
                if (Directory.Exists(destPath))
                {
                    Directory.Delete(destPath, true);
                }
            }
            catch (Exception) { }

            using (var tar = File.OpenRead(tarPath))
            {
                while (tar.Read(buffer, 0, 512) > 0)
                {
                    string filename = Encoding.ASCII.GetString(buffer, 0, 100).Trim((char)0);

                    if (!String.IsNullOrEmpty(filename))
                    {
                        long size = Convert.ToInt64(Encoding.ASCII.GetString(buffer, 124, 11).Trim(), 8);

                        if (size > 0)
                        {
                            string path = destPath + "/" + filename;
                            Directory.CreateDirectory(Path.GetDirectoryName(path));

                            using (var file = File.Create(path))
                            {
                                long blocks = (size + 511) / 512;
                                for (int i = 0; i < blocks; i++)
                                {
                                    tar.Read(buffer, 0, 512);
                                    file.Write(buffer, 0, (Int32)Math.Min(size, 512));
                                    size -= 512;
                                }
                            }
                        }
                    }
                }
            }

            AssetDatabase.DeleteAsset(Path.Combine("Assets", TarLocation));
        }
    }
}
