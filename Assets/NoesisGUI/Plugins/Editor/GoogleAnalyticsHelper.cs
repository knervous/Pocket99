using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Net;
using System.Threading;

public sealed class GoogleAnalyticsHelper
{
    private static string GetWindowsNTVersion(string UnityOSVersionName)
    {
        //https://en.wikipedia.org/wiki/Windows_NT
        if (UnityOSVersionName.Contains("(5.1"))
            return "Windows NT 5.1";
        else if (UnityOSVersionName.Contains("(5.2"))
            return "Windows NT 5.2";
        else if (UnityOSVersionName.Contains("(6.0"))
            return "Windows NT 6.0";
        else if (UnityOSVersionName.Contains("(6.1"))
            return "Windows NT 6.1";
        else if (UnityOSVersionName.Contains("(6.2"))
            return "Windows NT 6.2";
        else if (UnityOSVersionName.Contains("(6.3"))
            return "Windows NT 6.3";
        else if (UnityOSVersionName.Contains("(10.0"))
            return "Windows NT 10.0";
        else
            return "Windows";
    }

    private static string GetUserAgent()
    {
        string user_agent = "Unity/" + Application.unityVersion;

        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
        {
            user_agent +=
                " (Macintosh; " + (SystemInfo.processorType.Contains("Intel") ? "Intel " : "PPC ") + SystemInfo.operatingSystem.Replace(".", "_") + ")" +
                " Unity/" + Application.unityVersion +
                " Unity/" + Application.unityVersion;
        }
        else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            user_agent +=
                " (" + GetWindowsNTVersion(SystemInfo.operatingSystem) + (SystemInfo.operatingSystem.Contains("64bit") ? "; WOW64)" : ")") + 
                " Unity/" + Application.unityVersion + 
                " (KHTML, like Gecko) Unity/" + Application.unityVersion + 
                " Unity/" + Application.unityVersion;
        }
        else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Linux)
        {
            user_agent += " (Linux)";
        }

        return user_agent;
    }

    public static void LogEvent(string action, string label, int value)
    {
        //const string id = "UA-25866290-1";
        //const string domain = "127.0.0.1";
        //const string page = "Unity3D";
        //const string category = "NoesisGUI";
        //string culture = Thread.CurrentThread.CurrentCulture.Name;
        //string screenRes = Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString();

        //long utCookie = UnityEngine.Random.Range(10000000, 99999999);
        //long utRandom = UnityEngine.Random.Range(1000000000, 2000000000);
        //long utToday = GetEpochTime();
        //string encoded_equals = "%3D";
        //string encoded_separator = "%7C";
        //string _utma = utCookie + "." + utRandom + "." + utToday + "." + utToday + "." + utToday + ".2" + UnityWebRequest.EscapeURL(";") + UnityWebRequest.EscapeURL("+");
        //string cookieUTMZstr = "utmcsr" + encoded_equals + "(direct)" + encoded_separator + "utmccn" + encoded_equals + "(direct)" + encoded_separator + "utmcmd" + encoded_equals + "(none)" + UnityWebRequest.EscapeURL(";");
        //string _utmz = utCookie + "." + utToday + "2.2.2." + cookieUTMZstr;

        //var requestParams = new Hashtable();
        //requestParams.Add("utmwv", "4.6.5");
        //requestParams.Add("utmn", utRandom.ToString());
        //requestParams.Add("utmhn", UnityWebRequest.EscapeURL(domain));
        //requestParams.Add("utmcs", "-");
        //requestParams.Add("utmsr", screenRes);
        //requestParams.Add("utmsc", "-");
        //requestParams.Add("utmul", culture);
        //requestParams.Add("utmje", "-");
        //requestParams.Add("utmfl", "-");
        //requestParams.Add("utmdt", page);
        //requestParams.Add("utmhid", utRandom.ToString());
        //requestParams.Add("utmr", "0");
        //requestParams.Add("utmp", page);
        //requestParams.Add("utmac", id);
        //requestParams.Add("utmcc", "__utma" + encoded_equals + _utma + "__utmz" + encoded_equals + _utmz);

        //string eventparams = "5(" + category + "*" + action;
        //eventparams += "*" + label + ")(" + value.ToString() + ")";
        //requestParams.Add("utme", eventparams);
        //requestParams.Add("utmt", "event");

        //// Create query string
        //ArrayList pageURI = new ArrayList();
        //foreach (string key in requestParams.Keys)
        //{
        //    pageURI.Add(key + "=" + requestParams[key]);
        //}

        //using (var client = new WebClient())
        //{
        //    client.Headers.Add("user-agent", GetUserAgent());
        //    string url = "http://www.google-analytics.com/__utm.gif?" + string.Join("&", (string[])pageURI.ToArray(typeof(string)));
        //    client.DownloadData(url);
        //}
    }

    private static long GetEpochTime()
    {
        System.DateTime dtCurTime = System.DateTime.Now;
        System.DateTime dtEpochStartTime = System.Convert.ToDateTime("1/1/1970 0:00:00 AM");
        System.TimeSpan ts = dtCurTime.Subtract(dtEpochStartTime);

        return ((((((ts.Days * 24) + ts.Hours) * 60) + ts.Minutes) * 60) + ts.Seconds);
    }
}