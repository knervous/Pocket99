using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Linq;

/// <summary>
/// Post-processor for XAML and Fonts
/// </summary>
public class NoesisPostprocessor : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
        {
            return;
        }

        if (NoesisSettings.Get().postprocessorEnabled)
        {
            List<string> assets = new List<string>();
            assets.AddRange(importedAssets);
            assets.AddRange(movedAssets);

            // Do fonts first because XAML depends on the generated .asset
            foreach (var asset in assets)
            {
                if (HasExtension(asset, ".ttf") || HasExtension(asset, ".otf"))
                {
                    ImportFont(asset);
                }
            }

            // First, create all .asset resources to allow dependencies between XAMLs
            foreach (var asset in importedAssets)
            {
                if (HasExtension(asset, ".xaml"))
                {
                    CreateXamlAsset(asset);
                }
            }

            // And now, fully import each XAML
            foreach (var asset in importedAssets)
            {
                if (HasExtension(asset, ".xaml"))
                {
                    ImportXaml(asset);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }

    private static bool HasExtension(string filename, string extension)
    {
        return filename.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Imports a TrueType into a NoesisFont asset
    /// </summary>
    private static void ImportFont(string filename)
    {
        using (FileStream file = File.Open(filename, FileMode.Open))
        {
            string path = Path.ChangeExtension(filename, ".asset");
            NoesisFont font = AssetDatabase.LoadAssetAtPath<NoesisFont>(path);

            if (font == null)
            {
                font = (NoesisFont)ScriptableObject.CreateInstance(typeof(NoesisFont));
                font.source = filename;
                font.content = new byte[file.Length];
                file.Read(font.content, 0, (int)file.Length);
                AssetDatabase.CreateAsset(font, path);
            }
            else
            {
                font.content = new byte[file.Length];
                file.Read(font.content, 0, (int)file.Length);
                EditorUtility.SetDirty(font);
            }
        }
    }

    /// <summary>
    /// Unity always expects paths with forward slashes (/) and rooted at "Assets/"
    /// </summary>
    private static string NormalizePath(string uri)
    {
        string full = Path.GetFullPath(uri).Replace('\\', '/');
        return full.Substring(full.IndexOf("Assets/"));
    }

    /// <summary>
    /// In XAML, URIs are relative by default. Absolute URIs use the following syntaxes:
    ///     "pack://application:,,,/path1/path2/resource.ext"
    ///     "/ReferencedAssembly;component/path1/path2/resource.ext"
    ///     "/path1/path2/resource.ext"
    /// </summary>
    private static string AbsolutePath(string parent, string uri)
    {
        const string PackUri = "pack://application:,,,";
        int n = uri.IndexOf(PackUri);
        if (n != -1)
        {
            uri = uri.Substring(n + PackUri.Length);
        }

        const string ComponentUri = ";component";
        n = uri.IndexOf(ComponentUri);
        if (n != -1)
        {
            uri = uri.Substring(n + ComponentUri.Length);
        }

        if (uri.StartsWith("/"))
        {
            return NormalizePath(uri);
        }
        else
        {
            return NormalizePath(parent + "/" + uri);
        }
    }

    /// <summary>
    /// Returns all the usages found of the given keyword under quotation marks
    /// </summary>
    private static List<string> ScanKeyword(string text, string keyword)
    {
        List<string> strings = new List<string>();

        int cur = 0;
        int pos;

        while ((pos = text.IndexOf(keyword, cur, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            int start = pos;
            while (start >= 0 && text[start] != '\"' && text[start] != '\'' && text[start] != '>')
            {
                start--;
            }

            int end = pos;
            while (end < text.Length && text[end] != '\"' && text[end] != '\'' && text[end] != '<')
            {
                end++;
            }

            if (start >= 0 && end < text.Length)
            {
                strings.Add(text.Substring(start + 1, end - start - 1));
            }

            cur = pos + keyword.Length;
        }

        return strings;
    }

    private static void ScanTexture(string directory, string text, string extension, HashSet<string> dependencies)
    {
        List<string> keywords = ScanKeyword(text, extension);

        foreach (var keyword in keywords)
        {
            string path = AbsolutePath(directory, keyword);
            string guid = AssetDatabase.AssetPathToGUID(path);

            if (!String.IsNullOrEmpty(guid))
            {
                dependencies.Add(guid);
            }
        }
    }

    private static void ScanTextures(NoesisXaml xaml, string directory, string text)
    {
        HashSet<string> textures = new HashSet<string>();

        ScanTexture(directory, text, ".jpg", textures);
        ScanTexture(directory, text, ".tga", textures);
        ScanTexture(directory, text, ".png", textures);
        ScanTexture(directory, text, ".gif", textures);
        ScanTexture(directory, text, ".bmp", textures);

        int i = 0;
        xaml.textures = new UnityEngine.Texture[textures.Count];
        xaml.texturePaths = new string[textures.Count];

        foreach (var texture in textures)
        {
            string path = AssetDatabase.GUIDToAssetPath(texture);
            xaml.texturePaths[i] = path;
            xaml.textures[i++] = (Texture)AssetDatabase.LoadAssetAtPath(path, typeof(Texture));
        }
    }

    private static void FindFamilyNames(string directory, string family, HashSet<string> dependencies)
    {
        try
        {
            var files = Directory.GetFiles(directory)
                .Where(s => s.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) || 
                            s.EndsWith(".otf", StringComparison.OrdinalIgnoreCase));

            foreach (var filename in files)
            {
                using (FileStream file = File.Open(filename, FileMode.Open))
                {
                    if (NoesisUnity.HasFamily(file, family))
                    {
                        string asset = Path.ChangeExtension(NormalizePath(filename), ".asset");
                        string guid = AssetDatabase.AssetPathToGUID(asset);

                        if (!String.IsNullOrEmpty(guid))
                        {
                            dependencies.Add(guid);
                        }
                    }
                }
            }
        }
        catch(System.Exception)
        {
        }
    }

    private static void ScanFonts(NoesisXaml xaml, string directory, string text)
    {
        HashSet<string> fonts = new HashSet<string>();

        List<string> keywords = ScanKeyword(text, "#");
        foreach (var keyword in keywords)
        {
            int index = keyword.IndexOf('#');
            string folder = AbsolutePath(directory, keyword.Substring(0, index));
            string family = keyword.Substring(index + 1);
            FindFamilyNames(folder, family, fonts);
        }

        int i = 0;
        xaml.fonts = new NoesisFont[fonts.Count];

        foreach (var font in fonts)
        {
            string path = AssetDatabase.GUIDToAssetPath(font);
            xaml.fonts[i++] = (NoesisFont)AssetDatabase.LoadAssetAtPath(path, typeof(NoesisFont));
        }
    }

    private static void ScanXamls(NoesisXaml xaml, string directory, string text)
    {
        HashSet<string> xamls = new HashSet<string>();

        List<string> keywords = ScanKeyword(text, ".xaml");
        foreach (var keyword in keywords)
        {
            string uri = AbsolutePath(directory, Path.ChangeExtension(keyword, ".asset"));
            string guid = AssetDatabase.AssetPathToGUID(uri);

            if (!String.IsNullOrEmpty(guid))
            {
                xamls.Add(guid);
            }
        }

        int i = 0;
        xaml.xamls = new NoesisXaml[xamls.Count];

        foreach (var xaml_ in xamls)
        {
            string path = AssetDatabase.GUIDToAssetPath(xaml_);
            xaml.xamls[i++] = (NoesisXaml)AssetDatabase.LoadAssetAtPath(path, typeof(NoesisXaml));
        }
    }

    private static void ScanDependencies(NoesisXaml xaml, string directory)
    {
        string text = System.Text.Encoding.UTF8.GetString(xaml.content);

        // Remove comments
        Regex exp = new Regex("<!--(.*?)-->", RegexOptions.Singleline);
        text = exp.Replace(text, "");

        ScanTextures(xaml, directory, text);
        ScanFonts(xaml, directory, text);
        ScanXamls(xaml, directory, text);

        xaml.ReloadDependencies();
    }

    private static void ImportXaml(NoesisXaml xaml, string directory, FileStream file)
    {
        xaml.content = new byte[file.Length];
        file.Read(xaml.content, 0, (int)file.Length);

        ScanDependencies(xaml, directory);
    }

    private static void ImportXaml(string filename)
    {
        NoesisUnity.Init();

        string path = Path.ChangeExtension(filename, ".asset");
        NoesisXaml xaml = AssetDatabase.LoadAssetAtPath<NoesisXaml>(path);
        xaml.source = filename;

        using (FileStream file = File.Open(filename, FileMode.Open))
        {
            string directory = Path.GetDirectoryName(filename);
            ImportXaml(xaml, directory, file);
        }

        // It is important to show XAML error messages at the time the user is editing.
        // We force here a Load() with that purpose. We do it deferred though to avoid continuously
        // crashing Unity if Load() raises an unexception exception
        EditorApplication.CallbackFunction d = null;
        d = () =>
        {
            EditorApplication.update -= d;

            try
            {
                xaml.Load();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("<b>" + filename + "</b>: " + e.Message, xaml);
            }

            // Update thumbnail
            EditorUtility.SetDirty(xaml);
        };

        EditorApplication.update += d;
    }

    private static void CreateXamlAsset(string filename)
    {
        string uri = Path.ChangeExtension(filename, ".asset");

        if (AssetDatabase.LoadAssetAtPath<NoesisXaml>(uri) == null)
        {
            NoesisXaml xaml = (NoesisXaml)ScriptableObject.CreateInstance(typeof(NoesisXaml));
            AssetDatabase.CreateAsset(xaml, uri);
        }
    }
}
