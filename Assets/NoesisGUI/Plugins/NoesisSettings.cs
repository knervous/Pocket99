using UnityEngine;
using System.IO;

/// <summary>
/// Noesis global settings
/// </summary>
public class NoesisSettings: ScriptableObject
{
    public static NoesisSettings Get()
    {
        NoesisSettings settings = Resources.Load<NoesisSettings>("NoesisSettings");

#if UNITY_EDITOR
        if (settings == null)
        {
            settings = (NoesisSettings)ScriptableObject.CreateInstance(typeof(NoesisSettings));
            Directory.CreateDirectory(Application.dataPath + "/NoesisGUI/Settings/Resources");
            UnityEditor.AssetDatabase.CreateAsset(settings, "Assets/NoesisGUI/Settings/Resources/NoesisSettings.asset");
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
        return settings;
    }

    [Tooltip("Default theme dictionary containing default styles and resources for all controls")]
    public NoesisXaml defaultTheme;

    public enum TextureSize
    {
        _256x256,
        _512x512,
        _1024x1024,
        _2048x2048,
        _4096x4096
    }

    [Header("Text Rendering")]
    [Tooltip("Dimensions of texture used to cache glyphs")]
    public TextureSize glyphTextureSize = TextureSize._1024x1024;

    [Range(32, 256)]
    [Tooltip("Glyphs with size above this are rendered using triangles")]
    public uint glyphMeshTreshold = 96;

    [Header("Offscreen")]
    [Tooltip("Dimensions of offscreen textures (0 = automatic)")]
    public Vector2 offscreenTextureSize;

    [Tooltip("Number of offscreen textures created at startup")]
    public uint offscreenInitSurfaces = 0;

    [Tooltip("Maximum number of offscreen textures (0 = unlimited)")]
    public uint offscreenMaxSurfaces = 0;

    [Header("Editor Settings")]
    [Tooltip("Automatically imports XAMLs when being edited")]
    public bool postprocessorEnabled = true;
    [Tooltip("Enables generation of thumbnails and previews")]
    public bool previewEnabled = true;

    public enum LogVerbosity
    {
        Disabled
    }

    public LogVerbosity logVerbosity = LogVerbosity.Disabled;

    [Space(10)]
    [Tooltip("XAMLs to register at startup. They can be loaded using Noesis.GUI.LoadXaml() without referrencing them in scripts or copying to /Resources")]
    public NoesisXaml[] preloadedXamls;
}
