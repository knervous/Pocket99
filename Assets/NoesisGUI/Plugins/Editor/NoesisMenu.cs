using UnityEngine;
using UnityEditor;
using System.IO;

public class NoesisMenu
{
    [UnityEditor.MenuItem("Tools/NoesisGUI/About NoesisGUI...", false, 30000)]
    static void OpenAbout()
    {
        EditorWindow.GetWindow(typeof(NoesisAbout), true, "About NoesisGUI");
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Settings...", false, 30050)]
    static void OpenSettings()
    {
        Selection.activeObject = NoesisSettings.Get();
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Welcome Screen...", false, 30100)]
    static void OpenWelcome()
    {
        EditorWindow.GetWindow(typeof(NoesisWelcome), true, "Welcome to NoesisGUI!");
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Documentation", false, 30103)]
    static void OpenDocumentation()
    {
        string docPath = Application.dataPath + "/../NoesisDoc/index.html";

        if (File.Exists(docPath))
        {
            UnityEngine.Application.OpenURL("file://" + docPath);
        }
        else
        {
            UnityEngine.Application.OpenURL("http://www.noesisengine.com/docs");
        }
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Forums", false, 30104)]
    static void OpenForum()
    {
        UnityEngine.Application.OpenURL("http://forums.noesisengine.com/");
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Review...", false, 30105)]
    static void OpenReview()
    {
        EditorWindow.GetWindow(typeof(NoesisReview), true, "Support our development");
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Release Notes", false, 30150)]
    static public void OpenReleaseNotes()
    {
        string docPath = Application.dataPath + "/../NoesisDoc/Doc/Gui.Core.Changelog.html";

        if (File.Exists(docPath))
        {
            UnityEngine.Application.OpenURL("file://" + docPath);
        }
        else
        {
            UnityEngine.Application.OpenURL("http://www.noesisengine.com/docs/Gui.Core.Changelog.html");
        }
    }

    [UnityEditor.MenuItem("Tools/NoesisGUI/Report a bug", false, 30151)]
    static void OpenReportBug()
    {
        UnityEngine.Application.OpenURL("http://bugs.noesisengine.com/");
    }
}
