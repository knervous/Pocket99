using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

public class NoesisAbout : EditorWindow
{
    private Texture2D _banner;
    private Texture2D _bannerbg;
    private GUIStyle _bannerStyle;
    private string _version;

    private const int Width = 290;
    private const int Height = 180;

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Width, 60), "", _bannerStyle);

        GUILayout.BeginArea(new Rect(0, 0, Width, Height));
        GUILayout.BeginVertical();

        GUILayout.Space(4.0f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(_banner);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(18.0f);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUILayout.Label("Version " + _version, EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(12.0f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        if (GUILayout.Button("Release Notes", GUILayout.MinWidth(180)))
        {
            NoesisMenu.OpenReleaseNotes();
        }
        if (GUILayout.Button("Noesis Technologies", GUILayout.MinWidth(180)))
        {
            UnityEngine.Application.OpenURL("http://www.noesisengine.com");
        }
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(10.0f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("(c) 2013 Noesis Technologies S.L. All Rights Reserved", EditorStyles.miniLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void OnEnable()
    {
        minSize = new Vector2(Width, Height);
        maxSize = new Vector2(Width, Height);

        _banner = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/banner.png", typeof(Texture2D));
        _bannerbg = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/banner_bg.png", typeof(Texture2D));
        _version = NoesisVersion.Get();

        _bannerStyle = new GUIStyle();
        _bannerStyle.normal.background = _bannerbg;
    }
}