using UnityEditor;
using UnityEngine;
using System;
using System.Collections;


public class NoesisReview: EditorWindow
{
    private const string ReviewStatusKey = "NoesisReviewStatus";
    private const string ReviewDateKey = "NoesisReviewDate";
    private const int Width = 420;
    private const int Height = 250;
    private Texture2D _banner;
    private Texture2D _bannerbg;
    private GUIStyle _bannerStyle;

    private enum State
    {
        WaitingForReview = 2,
        Reviewed = 3
    }

    [InitializeOnLoad]
    public class Checker
    {
        static Checker()
        {
            EditorApplication.update += CheckReview;
        }

        static void CheckReview()
        {
            EditorApplication.update -= CheckReview;

            if (UnityEditorInternal.InternalEditorUtility.inBatchMode || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            if (!EditorPrefs.HasKey(ReviewStatusKey))
            {
                EditorPrefs.SetInt(ReviewStatusKey, (int)State.WaitingForReview);
                UpdateReviewDate();
            }

            State state = (State)EditorPrefs.GetInt(ReviewStatusKey);
            if (state == State.WaitingForReview)
            {
                uint now = GetElapsedDays(DateTime.Now);
                uint reviewDate = (uint)EditorPrefs.GetInt(ReviewDateKey, (int)now);

                if (now >= reviewDate)
                {
                    EditorWindow.GetWindow(typeof(NoesisReview), true, "Support our development");
                }
            }
        }
    }

    void OnEnable()
    {
        position = new Rect((Screen.currentResolution.width - Width) / 2, (Screen.currentResolution.height - Height) / 2, Width, Height);
        minSize = new Vector2(Width, Height);
        maxSize = new Vector2(Width, Height);

        _banner = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/banner.png", typeof(Texture2D));
        _bannerbg = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/NoesisGUI/Plugins/Editor/banner_bg.png", typeof(Texture2D));

        _bannerStyle = new GUIStyle();
        _bannerStyle.normal.background = _bannerbg;
    }

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

        GUILayout.BeginVertical();
        GUILayout.Space(8.0f);

        GUILayout.BeginHorizontal();
        GUILayout.Space(12.0f);

        GUILayout.BeginVertical();

        GUILayout.Label("Are you satisfied with NoesisGUI?", EditorStyles.label);
        GUILayout.Space(8.0f);

        GUILayout.Label("We would appreciate you taking the time to share your opinion.", EditorStyles.label);
        GUILayout.Label("Please consider writing a review in the Unity Asset Store.", EditorStyles.label);
        GUILayout.Label("User ratings are a valuable indicator for other possible customers.", EditorStyles.label);

        GUILayout.Space(8.0f);
        GUILayout.Label("A big hearty THANK YOU in advance :)", EditorStyles.label);

        GUILayout.Space(12.0f);
        GUILayout.BeginHorizontal();

        Color currentBgColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.5f, 1.0f, 0.5f);
        Color currentFontColor = GUI.contentColor;
        GUI.contentColor = Color.green;
        int currentFontSize = GUI.skin.button.fontSize;
        GUI.skin.button.fontSize = 16;
        FontStyle currentFontStyle = GUI.skin.button.fontStyle;
        GUI.skin.button.fontStyle = FontStyle.Bold;
        if (GUILayout.Button("Review now!", new GUILayoutOption[] {
            GUILayout.MinHeight(40.0f), GUILayout.MinWidth(150.0f) }))
        {
            GoogleAnalyticsHelper.LogEvent("Review", "Reviewed", 0);
            UnityEngine.Application.OpenURL("http://u3d.as/55A"); 
            EditorPrefs.SetInt(ReviewStatusKey, (int)State.Reviewed);
            Close();
        }
        GUI.backgroundColor = currentBgColor;
        GUI.contentColor = currentFontColor;
        GUI.skin.button.fontSize = currentFontSize;
        GUI.skin.button.fontStyle = currentFontStyle;

        if (GUILayout.Button("Remind me in 7 days", GUILayout.MinHeight(40.0f)))
        {
            GoogleAnalyticsHelper.LogEvent("Review", "Later", 0);
            UpdateReviewDate();
            Close();
        }

        if (GUILayout.Button("Don't ask again", GUILayout.MinHeight(40.0f)))
        {
            GoogleAnalyticsHelper.LogEvent("Review", "Declined", 0);
            EditorPrefs.SetInt(ReviewStatusKey, (int)State.Reviewed);
            Close();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(12.0f);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private static void UpdateReviewDate()
    {
        DateTime reviewDate = DateTime.Now.AddDays(7.0);
        uint elapsed = GetElapsedDays(reviewDate);
        EditorPrefs.SetInt(ReviewDateKey, (int)elapsed);
    }

    private static uint GetElapsedDays(DateTime date)
    {
        DateTime StartDate = new DateTime(2014, 1, 1);
        TimeSpan elapsed = new TimeSpan(date.Ticks - StartDate.Ticks);
        return (uint)elapsed.TotalDays;
    }
}
