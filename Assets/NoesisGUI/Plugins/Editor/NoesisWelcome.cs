using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class NoesisWelcome : EditorWindow
{
    public static void Open()
    {
        Rect r = new Rect((Screen.currentResolution.width - Width) / 2, (Screen.currentResolution.height - Height) / 2, Width, Height);
        EditorWindow.GetWindowWithRect(typeof(NoesisWelcome), r, true, "Welcome to NoesisGUI!");
    }

    private Texture2D _banner;
    private Texture2D _icon0;
    private Texture2D _icon1;
    private Texture2D _icon2;
    private Texture2D _icon3;
    private string _version;

    private const int Width = 330;
    private const int Height = 380;
    private GUIStyle _buttonStyle;
    private GUIStyle _bannerStyle;

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
        GUILayout.Label("NoesisGUI v" + _version + " installed", EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(18.0f);

        string docPath = Application.dataPath + "/../NoesisDoc/Documentation.html";
        string docURL;

        if (File.Exists(docPath))
        {
            docURL = "file://" + docPath;
        }
        else
        {
            docURL = "http://www.noesisengine.com/docs";
        }

        string changelogPath = Application.dataPath + "/../NoesisDoc/Doc/Gui.Core.Changelog.html";
        string changelogURL;

        if (File.Exists(changelogPath))
        {
            changelogURL = "file://" + changelogPath;
        }
        else
        {
            changelogURL = "http://www.noesisengine.com/docs/Gui.Core.Changelog.html";
        }

        Button(_icon0, "Release notes", "Read what is new in this version", changelogURL);
        GUILayout.Space(10.0f);
        Button(_icon1, "Examples", "Learn from our samples", "http://www.noesisengine.com/developers/samples.php");
        GUILayout.Space(10.0f);
        Button(_icon2, "Documentation", "Read local documentation", docURL);
        GUILayout.Space(10.0f);
        Button(_icon3, "Forums", "Join the noesisGUI community", "http://forums.noesisengine.com/");
        GUILayout.Space(10.0f);

        GUILayout.Space(8.0f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Find more options at Tools -> NoesisGUI", EditorStyles.miniLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void Button(Texture2D texture, string name, string desc, string url)
    {
        GUILayout.BeginHorizontal(GUILayout.MaxHeight(48));
        GUILayout.Space(38.0f);

        if (GUILayout.Button(texture, _buttonStyle))
        {
            UnityEngine.Application.OpenURL(url);
        }
        EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);

        GUILayout.Space(20.0f);
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        GUILayout.Label(name, EditorStyles.boldLabel);
        GUILayout.Label(desc, EditorStyles.label);
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    void OnEnable()
    {
        _version = NoesisVersion.Get();

        _bannerStyle = new GUIStyle();
        _buttonStyle = new GUIStyle();
        _buttonStyle.fixedWidth = 48;
        _buttonStyle.fixedHeight = 48;
    }

    void OnInspectorUpdate()
    {
        bool doRepaint = false;

        if (_banner == null)
        {
            _banner = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/banner.png", typeof(Texture2D));
            if (_banner != null) doRepaint = true;
        }

        if (_bannerStyle.normal.background == null)
        {
            _bannerStyle.normal.background = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/banner_bg.png", typeof(Texture2D));
            if (_bannerStyle.normal.background != null) doRepaint = true;
        }

        if (_icon0 == null)
        {
            _icon0 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/ico_release.png", typeof(Texture2D));
            if (_banner != null) doRepaint = true;
        }

        if (_icon1 == null)
        {
            _icon1 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/ico_tutorials.png", typeof(Texture2D));
            if (_banner != null) doRepaint = true;
        }

        if (_icon2 == null)
        {
            _icon2 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/ico_doc.png", typeof(Texture2D));
            if (_banner != null) doRepaint = true;
        }

        if (_icon3 == null)
        {
            _icon3 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/ico_forums.png", typeof(Texture2D));
            if (_banner != null) doRepaint = true;
        }

        if (doRepaint)
        {
            Repaint();
        }
    }
}