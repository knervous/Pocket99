using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Post-processor for XAMLs and Fonts
/// </summary>
public class NoesisPostprocessor : AssetPostprocessor
{
    public static void ImportAllAssets()
    {
        NoesisPostprocessor.ImportAllAssets((progress, asset) => EditorUtility.DisplayProgressBar("Reimport All XAMLs", asset, progress));
        EditorUtility.ClearProgressBar();
    }

    public delegate void UpdateProgress(float progress, string asset);

    public static void ImportAllAssets(UpdateProgress d)
    {
        var assets = AssetDatabase.FindAssets("")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Where(s => IsFont(s) || IsXaml(s))
            .Distinct().ToArray();

        NoesisPostprocessor.ImportAssets(assets, d);
    }


    private static void ImportAssets(string[] assets, UpdateProgress d)
    {
        int numFonts = assets.Count(asset => IsFont(asset));
        int numXamls = assets.Count(asset => IsXaml(asset));
        int numAssets = numFonts + numXamls;

        if (numAssets > 0)
        {
            Log("→ Import assets (XAMLs: " + numXamls + " Fonts: " + numFonts + ")");

            float delta = 1.0f / numAssets;
            float progress = 0.0f;

            if (numXamls > 0)
            {
                NoesisUnity.Init();

                // Theme
                NoesisXaml theme = NoesisSettings.Get().applicationResources;
                if (theme != null)
                {
                    Log("Scanning for theme changes...");

                    bool changed;
                    ImportXaml(theme.source, false, out changed);

                    if (changed)
                    {
                        Log("↔ Reload ApplicationResources");
                        NoesisUnity.LoadApplicationResources();
                    }
                }
            }

            foreach (var asset in assets)
            {
                try
                {
                    if (IsFont(asset))
                    {
                        ImportFont(asset, true);
                    }
                    else if (IsXaml(asset))
                    {
                        ImportXaml(asset, true);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

                if (d != null && (IsFont(asset) || IsXaml(asset)))
                {
                    d(progress, asset);
                    progress += delta;
                }
            }

            Log("← Import assets");
        }
    }

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (NoesisSettings.IsNoesisEnabled())
        {
            EditorApplication.CallbackFunction d = null;

            // Delay the import process to have all texture assets ready
            d = () =>
            {
                EditorApplication.update -= d;

                string[] assets = importedAssets.Concat(movedAssets).ToArray();
                NoesisPostprocessor.ImportAssets(assets, (progress, asset) => EditorUtility.DisplayProgressBar("Import XAMLs", asset, progress));
                EditorUtility.ClearProgressBar();
            };

            EditorApplication.update += d;
        }
    }

    private static bool HasExtension(string filename, string extension)
    {
        return filename.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsXaml(string filename)
    {
        return HasExtension(filename, ".xaml");
    }

    private static bool IsFont(string filename)
    {
        return HasExtension(filename, ".ttf") || HasExtension(filename, ".otf") || HasExtension(filename, ".ttc");
    }

    private static NoesisFont ImportFont(string filename, bool reimport)
    {
        string path = Path.ChangeExtension(filename, ".asset");
        NoesisFont font = AssetDatabase.LoadAssetAtPath<NoesisFont>(path);

        if (font == null)
        {
            Log("↔ Create " + filename);
            font = (NoesisFont)ScriptableObject.CreateInstance(typeof(NoesisFont));
            AssetDatabase.CreateAsset(font, path);
        }

        byte[] content = File.ReadAllBytes(filename);

        if (reimport || font.content == null || !font.content.SequenceEqual(content) || font.source != filename)
        {
            Log("→ ImportFont " + filename);
            font.source = filename;
            font.content = content;
            EditorUtility.SetDirty(font);
            AssetDatabase.SaveAssets();
            Log("← ImportFont");
        }

        return font;
    }

    private static void ScanFont(string uri, ref List<NoesisFont> fonts)
    {
        int index = uri.IndexOf('#');
        if (index != -1)
        {
            string folder = uri.Substring(0, index);
            if (Directory.Exists(folder))
            {
                string family = uri.Substring(index + 1);
                var files = Directory.GetFiles(folder).Where(s => IsFont(s));

                foreach (var font in files)
                {
                    bool hasFamily = false;

                    using (FileStream file = File.Open(font, FileMode.Open, FileAccess.Read))
                    {
                        hasFamily = NoesisUnity.HasFamily(file, family);
                    }

                    if (hasFamily)
                    {
                        fonts.Add(ImportFont(font, false));
                    }
                }
            }
        }
    }

    private static void ScanTexture(string uri, ref List<Texture> textures)
    {
        if (!String.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(uri)))
        {
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(uri);

            if (texture != null)
            {
                textures.Add(texture);
            }
        }
    }

    private static void ScanAudio(string uri, ref List<AudioClip> audios)
    {
        if (!String.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(uri)))
        {
            AudioClip audio = AssetDatabase.LoadAssetAtPath<AudioClip>(uri);

            if (audio != null)
            {
                audios.Add(audio);
            }
        }
    }

    private static void ScanXaml(string uri, ref List<NoesisXaml> xamls)
    {
        if (IsXaml(uri))
        {
            if (File.Exists(uri))
            {
                xamls.Add(ImportXaml(uri, false));
            }
        }
    }

    private static void ScanDependencies(string filename, out List<NoesisFont> fonts_, out List<Texture> textures_, out List<AudioClip> audios_, out List<NoesisXaml> xamls_)
    {
        List<NoesisFont> fonts = new List<NoesisFont>();
        List<Texture> textures = new List<Texture>();
        List<AudioClip> audios = new List<AudioClip>();
        List<NoesisXaml> xamls = new List<NoesisXaml>();

        try
        {
            using (FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                string directory = Path.GetDirectoryName(filename);
                Noesis.GUI.GetXamlDependencies(file, directory, (uri, type) =>
                {
                    try
                    {
                        if (type == Noesis.XamlDependencyType.Filename)
                        {
                            ScanXaml(uri, ref xamls);
                            ScanTexture(uri, ref textures);
                            ScanAudio(uri, ref audios);
                        }
                        else if (type == Noesis.XamlDependencyType.Font)
                        {
                            ScanFont(uri, ref fonts);
                        }
                        else if (type == Noesis.XamlDependencyType.UserControl)
                        {
                            string userControl = AssetDatabase.FindAssets("")
                                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                .Where(s => String.Equals(Path.GetFileName(s), uri + ".xaml", StringComparison.OrdinalIgnoreCase))
                                .FirstOrDefault();

                            if (!String.IsNullOrEmpty(userControl))
                            {
                                ScanXaml(userControl, ref xamls);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                         Debug.LogException(e);
                    }
                });
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        fonts_ = fonts;
        textures_ = textures;
        audios_ = audios;
        xamls_ = xamls;
    }

    private static NoesisXaml ImportXaml(string filename, bool reimport)
    {
        bool changed;
        return ImportXaml(filename, reimport, out changed);
    }

    private static NoesisXaml ImportXaml(string filename, bool reimport, out bool changed)
    {
        changed = false;
        string path = Path.ChangeExtension(filename, ".asset");
        NoesisXaml xaml = AssetDatabase.LoadAssetAtPath<NoesisXaml>(path);

        if (xaml == null)
        {
            Log("↔ Create " + filename);
            xaml = (NoesisXaml)ScriptableObject.CreateInstance(typeof(NoesisXaml));
            AssetDatabase.CreateAsset(xaml, path);
        }

        byte[] content = File.ReadAllBytes(filename);

        if (reimport || xaml.content == null || !xaml.content.SequenceEqual(content) || xaml.source != filename)
        {
            Log("→ ImportXaml " + filename);
            changed = true;

            xaml.source = filename;
            xaml.content = content;

            List<NoesisFont> fonts;
            List<Texture> textures;
            List<AudioClip> audios;
            List<NoesisXaml> xamls;
            xaml.UnregisterDependencies();
            ScanDependencies(filename, out fonts, out textures, out audios, out xamls);

            xaml.xamls = xamls.ToArray();
            xaml.textures = textures.Select(t => new NoesisXaml.Texture { uri = AssetDatabase.GetAssetPath(t), texture = t} ).ToArray();
            xaml.audios = audios.Select(t => new NoesisXaml.Audio { uri = AssetDatabase.GetAssetPath(t), audio = t} ).ToArray();
            xaml.fonts = fonts.ToArray();
            xaml.RegisterDependencies();

            EditorUtility.SetDirty(xaml);
            AssetDatabase.SaveAssets();
            Log("← ImportXaml");
        }
        else
        {
            // XAML didn't change, let's continue scanning its dependencies
            foreach (var dep in xaml.xamls)
            {
                if (File.Exists(dep.source))
                {
                    bool changed_;
                    ImportXaml(dep.source, false, out changed_);
                    changed = changed || changed_;
                }
            }

            foreach (var dep in xaml.fonts)
            {
                if (File.Exists(dep.source))
                {
                    ImportFont(dep.source, false);
                }
            }
        }

        if (reimport)
        {
            // Show parsing errors in the console
            try
            {
                xaml.Load();
            }
            catch (Exception e)
            {
                Debug.LogException(e, xaml);
            }
        }

        return xaml;
    }

    private void OnPostprocessTexture(Texture2D texture)
    {
        if (AssetDatabase.GetLabels(assetImporter).Contains("Noesis"))
        {
            Color[] c = texture.GetPixels(0);

            // NoesisGUI needs premultipled alpha
            for (int i = 0; i < c.Length; i++)
            {
                c[i].r = c[i].r * c[i].a;
                c[i].g = c[i].g * c[i].a;
                c[i].b = c[i].b * c[i].a;
            }

            // Set new content and make the texture unreadable at runtime
            texture.SetPixels(c, 0);
            texture.Apply(true, true);
        }
    }

    private void OnPreprocessTexture()
    {
        if (AssetDatabase.GetLabels(assetImporter).Contains("Noesis"))
        {
            // If the texture is going to be modified it is required to be readable
            TextureImporter importer = (TextureImporter)assetImporter;
            importer.isReadable = true;
        }
    }

    private static void Log(string message)
    {
        if (NoesisSettings.Get().debugImporter)
        {
            Debug.Log(message);
        }
    }
}
