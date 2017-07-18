using UnityEngine;
using Noesis;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class NoesisVersion
{
    private static string Version = "2.0.2f2";
    private static string VersionFilename = Application.dataPath + "/NoesisGUI/Plugins/Editor/version.txt";

    public static string GetCached()
    {
        string version = "";

        try
        {
            if (File.Exists(VersionFilename))
            {
                using (var reader = new StreamReader(VersionFilename))
                {
                    version = reader.ReadLine();
                }
            }
        }
        catch (Exception) { }

        return version;
    }

    public static void SetCached(string version)
    {
        try
        {
            using (var writer = new StreamWriter(VersionFilename))
            {
                writer.WriteLine(version);
            }
        }
        catch (Exception) { }
    }

    public static string Get()
    {
        return Version;
    }

    public static bool RestartNeeded()
    {
        return Version != "0.0.0" && Noesis.GUI.GetBuildVersion() != Version;
    }
}