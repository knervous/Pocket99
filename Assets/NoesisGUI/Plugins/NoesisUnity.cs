using Noesis;
using UnityEngine;
using System.Runtime.InteropServices;

public class NoesisUnity
{
    private static bool _initialized = false;

    public static void Init()
    {
        if (!_initialized)
        {
            _initialized = true;

            Noesis.GUI.Init();
            Noesis.GUI.SoftwareKeyboard = new UnitySoftwareKeyboard();
            RegisterProviders();
            LoadDefaultTheme();

            PatchMono();
        }
    }

    private static void LoadDefaultTheme()
    {
        try
        {
            var settings = NoesisSettings.Get();

            if (settings.defaultTheme != null)
            {
                ResourceDictionary theme = settings.defaultTheme.Load() as ResourceDictionary;
                if (theme != null)
                {
                    Noesis.GUI.SetTheme(theme);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Invalid global theme in settings");
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }

    private static void RegisterProviders()
    {
        Provider p = new Provider()
        {
            XamlProvider = NoesisXamlProvider.instance,
            TextureProvider = NoesisTextureProvider.instance,
            FontProvider = NoesisFontProvider.instance
        };

        Noesis.GUI.SetResourceProvider(p);
    }

    public static bool HasFamily(System.IO.Stream stream, string family)
    {
        bool hasFamily = Noesis_HasFamily(Extend.GetInstanceHandle(stream).Handle, family);
        Error.Check();

        return hasFamily;
    }

    private static void PatchMono()
    {
        Noesis_PatchMono();
    }

    #region Imports

    #if UNITY_IPHONE || UNITY_XBOX360
    [DllImport("__Internal")]
    #else
    [DllImport("Noesis")]
    #endif
    private static extern void Noesis_PatchMono();

    #if UNITY_IPHONE || UNITY_XBOX360
    [DllImport("__Internal")]
    #else
    [DllImport("Noesis")]
    #endif
    static extern bool Noesis_HasFamily(System.IntPtr stream, string family);

    #endregion
}