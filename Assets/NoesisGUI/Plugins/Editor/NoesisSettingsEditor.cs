using UnityEngine;
using UnityEditor;
using Noesis;

[CustomEditor(typeof(NoesisSettings))]
public class NoesisSettingsEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(25);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Reimport All", GUILayout.MaxWidth(175), GUILayout.MinHeight(20)))
        {
            string[] guids = AssetDatabase.FindAssets ("t:NoesisXaml");
            float progress = 0.0f;

            foreach (var guid in guids)
            {
                string path = System.IO.Path.ChangeExtension(AssetDatabase.GUIDToAssetPath(guid), ".xaml");

                EditorUtility.DisplayProgressBar("Reimport All XAMLs", path, progress);
                progress += 1.0f / guids.Length;

                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.ImportAsset(path);
                }
            }
        }
        else
        {
            EditorUtility.ClearProgressBar();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
