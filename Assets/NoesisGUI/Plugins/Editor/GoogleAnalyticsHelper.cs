using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;


// Google Analytics Helper
// Based on http://g3zarstudios.com/blog/google-analytics-in-unity/
public sealed class GoogleAnalyticsHelper
{
    public static void LogEvent(string action, string label, int value)
    {
        const string id = "UA-25866290-1";
        const string domain = "127.0.0.1";
        const string page = "Unity3D";
        const string category = "NoesisGUI";

        long utCookie = UnityEngine.Random.Range(10000000, 99999999);
        long utRandom = UnityEngine.Random.Range(1000000000, 2000000000);
        long utToday = GetEpochTime();
        string encoded_equals = "%3D";
        string encoded_separator = "%7C";
        string _utma = utCookie + "." + utRandom + "." + utToday + "." + utToday + "." + utToday + ".2" + WWW.EscapeURL(";") + WWW.EscapeURL("+");
        string cookieUTMZstr = "utmcsr" + encoded_equals + "(direct)" + encoded_separator + "utmccn" + encoded_equals + "(direct)" + encoded_separator + "utmcmd" + encoded_equals + "(none)" + WWW.EscapeURL(";");
        string _utmz = utCookie + "." + utToday + "2.2.2." + cookieUTMZstr;

        var requestParams = new Hashtable();
        requestParams.Add("utmwv", "4.6.5");
        requestParams.Add("utmn", utRandom.ToString());
        requestParams.Add("utmhn", WWW.EscapeURL(domain));
        requestParams.Add("utmcs", "ISO-8859-1");
        requestParams.Add("utmsr", Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString());
        requestParams.Add("utmsc", "24-bit");
        requestParams.Add("utmul", "nl");
        requestParams.Add("utmje", "0");
        requestParams.Add("utmfl", "-");
        requestParams.Add("utmdt", WWW.EscapeURL(page));
        requestParams.Add("utmhid", utRandom.ToString());
        requestParams.Add("utmr", "-");
        requestParams.Add("utmp", page);
        requestParams.Add("utmac", id);
        requestParams.Add("utmcc", "__utma" + encoded_equals + _utma + "__utmz" + encoded_equals + _utmz);

        // Add event if available:
        if (category.Length > 0 && action.Length > 0)
        {
            string eventparams = "5(" + category + "*" + action;
            if (label.Length > 0)
            {
                eventparams += "*" + label + ")(" + value.ToString() + ")";
            }
            else
            {
                eventparams += ")";
            }
            requestParams.Add("utme", eventparams);
            requestParams.Add("utmt", "event");
        }

        // Create query string:
        ArrayList pageURI = new ArrayList();
        foreach (string key in requestParams.Keys)
        {
            pageURI.Add(key + "=" + requestParams[key]);
        }

        string url = "http://www.google-analytics.com/__utm.gif?" + string.Join("&", (string[])pageURI.ToArray(typeof(string)));
        // Debug.Log(url);
        new WWW(url);
    }

    private static long GetEpochTime()
    {
        System.DateTime dtCurTime = System.DateTime.Now;
        System.DateTime dtEpochStartTime = System.Convert.ToDateTime("1/1/1970 0:00:00 AM");
        System.TimeSpan ts = dtCurTime.Subtract(dtEpochStartTime);

        return ((((((ts.Days * 24) + ts.Hours) * 60) + ts.Minutes) * 60) + ts.Seconds);
    }
}