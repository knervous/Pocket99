using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

[InitializeOnLoad]
public class NoesisUpdater
{
    static NoesisUpdater()
    {
        EditorApplication.update += CheckVersion;
    }

    private static void CheckVersion()
    {
        EditorApplication.update -= CheckVersion;

        if (!UnityEditorInternal.InternalEditorUtility.inBatchMode)
        {
            string localVersion = NoesisVersion.GetCached();
            string version = NoesisVersion.Get();

            // Remove the file that indicates Noesis package is being installed
            AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/Editor/installing");

            // Detect if /Library is being recreated
            string noesisFile = Path.Combine(Application.dataPath, "../Library/noesis");
            bool libraryFolderRecreated = !File.Exists(noesisFile);
            if (libraryFolderRecreated)
            {
                File.Create(noesisFile).Dispose();
            }

            if (localVersion != version && version != "0.0.0")
            {
                if (NoesisVersion.RestartNeeded())
                {
                    Debug.LogWarning("Please restart Unity to reload NoesisGUI native plugin! " + 
                        "If error persists remove 'Assets/NoesisGUI/Plugins' and reimport again.");
                    return;
                }

                string title;

                if (localVersion != "")
                {
                    title = "Upgrading NoesisGUI " + localVersion + " -> " + version;
                }
                else
                {
                    title = "Installing NoesisGUI " +  version;
                }

                EditorUtility.DisplayProgressBar(title, "", 0.0f);
                GoogleAnalyticsHelper.LogEvent("Install", version, 0);

                EditorUtility.DisplayProgressBar(title, "Upgrading project", 0.10f);
                Upgrade(localVersion);

                EditorUtility.DisplayProgressBar(title, "Updating version", 0.20f);
                NoesisVersion.SetCached(version);

                EditorUtility.DisplayProgressBar(title, "Creating default settings", 0.35f);
                NoesisSettings.Get();

                EditorUtility.DisplayProgressBar(title, "Extracting documentation...", 0.40f);
                ExtractDocumentation();

                NoesisPostprocessor.ImportAllAssets((progress, asset) =>
                {
                    EditorUtility.DisplayProgressBar(title, asset, 0.60f + progress * 0.40f);
                });

                EditorApplication.update += ShowWelcomeWindow;
                EditorUtility.ClearProgressBar();

                Debug.Log("NoesisGUI v" + version + " successfully installed");
            }
            else if (libraryFolderRecreated)
            {
                NoesisPostprocessor.ImportAllAssets();
            }
        }
    }

    private static void ShowWelcomeWindow()
    {
        EditorApplication.update -= ShowWelcomeWindow;
        NoesisWelcome.Open();
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

        if (PatchNeeded(version, "2.1.0b10"))
        {
            Upgrade_2_1_0_b10();
        }

        if (PatchNeeded(version, "2.1.0rc4"))
        {
            Upgrade_2_1_0_rc4();
        }
        
        if (PatchNeeded(version, "2.2.0b6"))
        {
            Upgrade_2_2_0_b6();
        }

        RemoveEmptyScripts();
    }

    private static void Upgrade_2_1_0_b10()
    {
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Common");
    }

    private static void Upgrade_2_1_0_rc4()
    {
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/TicTacToe");
    }

    private static void Upgrade_2_2_0_b6()
    {
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Buttons/ControlResources.xaml");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Buttons/ControlResources.asset");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Buttons/LogoResources.xaml", "Assets/NoesisGUI/Samples/Buttons/Resources.xaml");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Buttons/LogoResources.asset", "Assets/NoesisGUI/Samples/Buttons/Resources.asset");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Buttons/ElementExtensions.cs");

        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/ControlGallery/Resources", "Assets/NoesisGUI/Samples/ControlGallery/Data");

        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Localization/ControlResources.xaml");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Localization/ControlResources.asset");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Localization/LogoResources.xaml", "Assets/NoesisGUI/Samples/Localization/Resources.xaml");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Localization/LogoResources.asset", "Assets/NoesisGUI/Samples/Localization/Resources.asset");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Localization/rounded-mgenplus-1c-regular.ttf");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Localization/rounded-mgenplus-1c-regular.asset");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Localization/XamlDependencies.cs");
        
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Login/ControlResources.xaml");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Login/ControlResources.asset");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Login/ElementExtensions.cs");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Login/LogoResources.xaml", "Assets/NoesisGUI/Samples/Login/Resources.xaml");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Login/LogoResources.asset", "Assets/NoesisGUI/Samples/Login/Resources.asset");

        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/QuestLog/ElementExtensions.cs");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/QuestLog/LogoResources.xaml", "Assets/NoesisGUI/Samples/QuestLog/Resources.xaml");
        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/QuestLog/LogoResources.asset", "Assets/NoesisGUI/Samples/QuestLog/Resources.asset");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/QuestLog/Images/QuestImages.xaml");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/QuestLog/Images/QuestImages.asset");

        AssetDatabase.MoveAsset("Assets/NoesisGUI/Samples/Scoreboard/Game.cs", "Assets/NoesisGUI/Samples/Scoreboard/ViewModel.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Samples/Scoreboard/Player.cs");
    }

    private static void RemoveEmptyScripts()
    {
        // From time to time we need to rename scripts (for example Unity doesn't like having a script called Grid.cs)
        // As there is no way to do the rename when instaling the unity package we need to do this trick: in the 
        // unity package both the renamed script and the original one (empty) are included. That way, the compilation
        // phase will succeed. After that, just at this point we remove the empty scripts
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Collection.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/FrameworkOptions.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/FreezableCollection.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Grid.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/KeyState.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Matrix2.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Matrix3.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/MouseState.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Pointi.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Recti.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Sizei.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/TimelineEventArgs.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Transform2.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Transform3.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/API/Proxies/Vector3.cs");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Plugins/NoesisSoftwareKeyboard.cs");
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
