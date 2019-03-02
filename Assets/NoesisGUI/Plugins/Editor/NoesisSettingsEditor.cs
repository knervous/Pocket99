using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(NoesisSettings))]
public class NoesisSettingsEditor: Editor
{
    public override void OnInspectorGUI()
    {
        OnInspectorGUI(new SerializedObject((NoesisSettings)target));
    }

    private static bool _showCursors = false;

    public static void OnInspectorGUI(SerializedObject settings)
    {
        EditorGUILayout.PropertyField(settings.FindProperty("applicationResources"));

        EditorGUILayout.PropertyField(settings.FindProperty("glyphTextureSize"));
        EditorGUILayout.PropertyField(settings.FindProperty("colorGlyphTextureSize"));
        EditorGUILayout.PropertyField(settings.FindProperty("glyphMeshThreshold"));
        EditorGUILayout.PropertyField(settings.FindProperty("offscreenSampleCount"));
        EditorGUILayout.PropertyField(settings.FindProperty("offscreenInitSurfaces"));
        EditorGUILayout.PropertyField(settings.FindProperty("offscreenMaxSurfaces"));
        EditorGUILayout.PropertyField(settings.FindProperty("linearRendering"));

        EditorGUILayout.PropertyField(settings.FindProperty("previewEnabled"));
        EditorGUILayout.PropertyField(settings.FindProperty("debugImporter"));
        EditorGUILayout.PropertyField(settings.FindProperty("logVerbosity"));

        GUILayout.Space(10);
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        _showCursors = EditorGUILayout.Foldout(_showCursors, "Cursors", false, EditorStyles.foldout);
        if (_showCursors)
        {
            EditorStyles.foldout.fontStyle = FontStyle.Normal;
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(settings.FindProperty("AppStarting"), new GUIContent("AppStarting"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("Arrow"), new GUIContent("Arrow"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ArrowCD"), new GUIContent("ArrowCD"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("Cross"), new GUIContent("Cross"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("Hand"), new GUIContent("Hand"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("Help"), new GUIContent("Help"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("IBeam"), new GUIContent("IBeam"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("No"), new GUIContent("No"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("None"), new GUIContent("None"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("Pen"), new GUIContent("Pen"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollAll"), new GUIContent("ScrollAll"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollE"), new GUIContent("ScrollE"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollN"), new GUIContent("ScrollN"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollNE"), new GUIContent("ScrollNE"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollNS"), new GUIContent("ScrollNS"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollNW"), new GUIContent("ScrollNW"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollS"), new GUIContent("ScrollS"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollSE"), new GUIContent("ScrollSE"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollSW"), new GUIContent("ScrollSW"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollW"), new GUIContent("ScrollW"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("ScrollWE"), new GUIContent("ScrollWE"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("SizeAll"), new GUIContent("SizeAll"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("SizeNESW"), new GUIContent("SizeNESW"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("SizeNS"), new GUIContent("SizeNS"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("SizeNWSE"), new GUIContent("SizeNWSE"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("SizeWE"), new GUIContent("SizeWE"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("UpArrow"), new GUIContent("UpArrow"), true);
            EditorGUILayout.PropertyField(settings.FindProperty("Wait"), new GUIContent("Wait"), true);
            EditorGUI.indentLevel--;
        }

        GUILayout.Space(10);
        EditorGUILayout.HelpBox("(*) Unity restart needed for updates to take effect", MessageType.None);

        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if(GUILayout.Button("Reimport All XAMLs", GUILayout.MaxWidth(175), GUILayout.MinHeight(20)))
        {
            NoesisPostprocessor.ImportAllAssets();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        settings.ApplyModifiedProperties();
    }

#if UNITY_2018_3_OR_NEWER
    [SettingsProvider]
    public static SettingsProvider CreateNoesisSettingsProvider()
    {
        var provider = new SettingsProvider("Project/NoesisSettings", SettingsScope.Project)
        {
            label = "NoesisGUI",
            guiHandler = (searchContext) =>
            {
                OnInspectorGUI(new SerializedObject(NoesisSettings.Get()));
            },

            keywords = new HashSet<string>(new[] { "Application Resources", "Glyph Texture Size",
                "Color Glyph Texture Size", "Glyph Mesh Threshold", "Offscreen Sample Count",
                "Offscreen Init Surfaces", "Offscreen Max Surfaces", "Linear Rendering", "Preview Enabled",
                "Debug Importer", "Log Verbosity", "AppStarting", "Arrow", "ArrowCD", "Cross", "Hand",
                "Help", "IBeam", "No", "None", "Pen", "ScrollAll", "ScrollE", "ScrollN", "ScrollNE",
                "ScrollNS", "ScrollNW", "ScrollS", "ScrollSE", "ScrollSW", "ScrollW", "ScrollWE",
                "SizeAll", "SizeNESW", "SizeNS", "SizeNWSE", "SizeWE", "UpArrow", "Wait" })
        };

        return provider;
    }
#endif
}