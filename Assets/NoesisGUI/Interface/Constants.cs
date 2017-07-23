#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Diagnostics;

#endif
namespace UserInterface
{
    public class Constants
    {
#if NOESIS
#if UNITY_EDITOR
        public static int WinHeight = UnityEngine.Screen.height;
        public static int WinWidth = UnityEngine.Screen.width;
        public static float WinHyp = UnityEngine.Mathf.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        public static int WinHeight = UnityEngine.Screen.width;
        public static int WinWidth = UnityEngine.Screen.height;
        public static float WinHyp = UnityEngine.Mathf.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#endif
#else
        public static int WinHeight = 720;
        public static int WinWidth = 1280;
        public static double WinHyp = Math.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#endif

    }



}