using Noesis;
using UnityEngine;
using System.Runtime.InteropServices;

public class NoesisUnity
{
    private static bool _initialized = false;
    static NoesisSettings _settings;

    public static void Init()
    {
        if (!_initialized)
        {
            _initialized = true;

            Noesis.GUI.Init();

            // Cache this because Unity is crashing internally if we access NoesisSettings from C++ callbacks
            _settings = NoesisSettings.Get();

            RegisterLog();
            RegisterError();
            RegisterProviders();
            LoadApplicationResources();

            Noesis.GUI.SetSoftwareKeyboardCallback(SoftwareKeyboard);
            Noesis.GUI.SetCursorCallback(UpdateCursor);
            Noesis.GUI.SetOpenUrlCallback(OpenUrl);
            Noesis.GUI.SetPlaySoundCallback(PlaySound);
        }
    }

    public static bool HasFamily(System.IO.Stream stream, string family)
    {
        return Noesis_HasFamily(Extend.GetInstanceHandle(stream).Handle, family);
    }

    public static void LoadApplicationResources()
    {
        try
        {
            if (_settings.applicationResources != null)
            {
                ResourceDictionary resources = _settings.applicationResources.Load() as ResourceDictionary;
                if (resources != null)
                {
                    Noesis.GUI.SetApplicationResources(resources);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Invalid Application Resources in settings");
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
        Noesis.GUI.SetXamlProvider(NoesisXamlProvider.instance);
        Noesis.GUI.SetTextureProvider(NoesisTextureProvider.instance);
        Noesis.GUI.SetFontProvider(NoesisFontProvider.instance);
    }

    #region Log management
    private static void RegisterLog()
    {
        Noesis_RegisterUnityLogCallbacks(_unityLog, _unityVerbosity);
#if UNITY_EDITOR
        System.AppDomain.CurrentDomain.DomainUnload += (sender, e) =>
        {
            Noesis_RegisterUnityLogCallbacks(null, null);
        };
#endif
    }

    private delegate void UnityLogCallback(int level, [MarshalAs(UnmanagedType.LPWStr)]string message);
    private static UnityLogCallback _unityLog = UnityLog;

    public static void MuteLog()
    {
        _muted = true;
    }

    public static void UnmuteLog()
    {
        _muted = false;
    }

    private static bool _muted = false;

    [MonoPInvokeCallback(typeof(UnityLogCallback))]
    private static void UnityLog(int level, string message)
    {
        if (!_muted)
        {
            switch ((LogLevel)level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Info:
                {
                    Debug.Log("[noesis] " + message);
                    break;
                }
                case LogLevel.Warning:
                {
                    Debug.LogWarning("[noesis] " + message);
                    break;
                }
                case LogLevel.Error:
                {
                    Debug.LogError("[noesis] " + message);
                    break;
                }
                default: break;
            }
        }
    }

    private delegate int UnityVerbosityCallback();
    private static UnityVerbosityCallback _unityVerbosity = UnityVerbosity;

    [MonoPInvokeCallback(typeof(UnityVerbosityCallback))]
    private static int UnityVerbosity()
    {
        return (int)_settings.logVerbosity;
    }
    #endregion

    #region Unhandled error management
    private static void RegisterError()
    {
        Error.SetUnhandledCallback(OnUnhandledException);
    }

    private static void OnUnhandledException(System.Exception e)
    {
        UnityEngine.Debug.LogException(e);
    }
    #endregion

    #region Software Keyboard
    public class IME
    {
        static public TextBox focused;
        static public string compositionString = "";

        static public void Open(UIElement focused_)
        {
            focused = (TextBox)focused_;
            Input.imeCompositionMode = IMECompositionMode.On;
            UpdateCursor();
        }

        static public void Close()
        {
            focused = null;
            Input.imeCompositionMode = IMECompositionMode.Off;
        }

        static public void Update(Noesis.View view)
        {
            if (focused != null)
            {
                if (focused.View == view)
                {
                    if (compositionString != Input.compositionString)
                    {
                        if (Input.compositionString == "")
                        {
                            UpdateCursor();
                        }
                        else
                        {
                            focused.SelectedText = Input.compositionString;
                        }

                        compositionString = Input.compositionString;
                    }
                }
            }
        }

        static private void UpdateCursor()
        {
            Noesis.Rect rect = focused.GetRangeBounds((uint)focused.CaretIndex, (uint)focused.CaretIndex);
            Matrix4 m = focused.TextView.TransformToAncestor(focused.View.Content);
            Noesis.Vector4 v = new Noesis.Vector4(rect.Location.X, rect.Location.Y + rect.Size.Height, 0, 1) * m;
            Input.compositionCursorPos = new UnityEngine.Vector2(v.X, v.Y + 25);
        }
    }

    public class TouchKeyboard
    {
        static public UIElement focused;
        static public string undoString;
        static public TouchScreenKeyboard keyboard;

        public static void Open(UIElement focused_)
        {
            string text = "";
            TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default;
            bool multiline = false;
            bool secure = false;
            int characterLimit = 0;

            if (focused_ is FrameworkElement)
            {
                switch (((FrameworkElement)focused_).InputScope)
                {
                    case InputScope.Url:
                        keyboardType = TouchScreenKeyboardType.URL;
                        break;
                    case InputScope.Digits:
                    case InputScope.Number:
                    case InputScope.NumberFullWidth:
                        keyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
                        break;
                    case InputScope.TelephoneNumber:
                    case InputScope.TelephoneLocalNumber:
                        keyboardType = TouchScreenKeyboardType.PhonePad;
                        break;
                    case InputScope.AlphanumericFullWidth:
                    case InputScope.AlphanumericHalfWidth:
                        keyboardType = TouchScreenKeyboardType.NamePhonePad;
                        break;
                    case InputScope.EmailSmtpAddress:
                        keyboardType = TouchScreenKeyboardType.EmailAddress;
                        break;
                    case InputScope.RegularExpression:
                        keyboardType = TouchScreenKeyboardType.Search;
                        break;
                    default:
                        keyboardType = TouchScreenKeyboardType.Default;
                        break;
                }
            }

            TextBox textBox = focused_ as TextBox;
            PasswordBox passwordBox = focused_ as PasswordBox;

            if (textBox != null)
            {
                text = textBox.Text;
                multiline = textBox.TextWrapping == TextWrapping.Wrap && textBox.AcceptsReturn;
                characterLimit = textBox.MaxLength;
                textBox.HideCaret();
            }
            else if (passwordBox != null)
            {
                text = passwordBox.Password;
                secure = true;
                passwordBox.HideCaret();
            }

            //keyboard = TouchScreenKeyboard.Open(text, keyboardType, true, multiline, secure, false, "", characterLimit);
            focused = focused_;
            undoString = text;
        }

        public static void Update()
        {
            if (keyboard != null)
            {
                TouchScreenKeyboard.Status status = keyboard.status;

                if (status == TouchScreenKeyboard.Status.Visible || status == TouchScreenKeyboard.Status.Done || status == TouchScreenKeyboard.Status.LostFocus)
                {
                    if (focused is TextBox)
                    {
                        ((TextBox)focused).Text = keyboard.text;
                    }
                    else if (focused is PasswordBox)
                    {
                        ((PasswordBox)focused).Password = keyboard.text;
                    }
                }
                else if (status == TouchScreenKeyboard.Status.Canceled)
                {
                    if (focused is TextBox)
                    {
                        ((TextBox)focused).Text = undoString;
                    }
                    else if (focused is PasswordBox)
                    {
                        ((PasswordBox)focused).Password = undoString;
                    }
                }

                if (status != TouchScreenKeyboard.Status.Visible)
                {
                    keyboard = null;
                    focused.Keyboard.Focus(null);
                }
            }
        }

        public static void Close()
        {
            if (keyboard != null)
            {
                keyboard.active = false;
            }
        }
    }

    private static void SoftwareKeyboard(UIElement focused, bool open)
    {
        if (TouchScreenKeyboard.isSupported)
        {
            if (open)
            {
                TouchKeyboard.Open(focused);
            }
            else
            {
                TouchKeyboard.Close();
            }
        }
        else if (focused is TextBox)
        {
            if (open)
            {
                IME.Open(focused);
            }
            else
            {
                IME.Close();
            }
        }
    }
    #endregion

    #region Cursor management
    private static void UpdateCursor(View view, Noesis.Cursor cursor)
    {
        NoesisSettings settings = NoesisSettings.Get();

        switch (cursor)
        {
            case Noesis.Cursor.AppStarting:
                UnityEngine.Cursor.SetCursor(settings.AppStarting.Texture, settings.AppStarting.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.Arrow:
                UnityEngine.Cursor.SetCursor(settings.Arrow.Texture, settings.Arrow.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ArrowCD:
                UnityEngine.Cursor.SetCursor(settings.ArrowCD.Texture, settings.ArrowCD.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.Cross:
                UnityEngine.Cursor.SetCursor(settings.Cross.Texture, settings.Cross.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.Hand:
                UnityEngine.Cursor.SetCursor(settings.Hand.Texture, settings.Hand.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.Help:
                UnityEngine.Cursor.SetCursor(settings.Help.Texture, settings.Help.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.IBeam:
                UnityEngine.Cursor.SetCursor(settings.IBeam.Texture, settings.IBeam.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.No:
                UnityEngine.Cursor.SetCursor(settings.No.Texture, settings.No.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.None:
                UnityEngine.Cursor.SetCursor(settings.None.Texture, settings.None.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.Pen:
                UnityEngine.Cursor.SetCursor(settings.Pen.Texture, settings.Pen.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollAll:
                UnityEngine.Cursor.SetCursor(settings.ScrollAll.Texture, settings.ScrollAll.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollE:
                UnityEngine.Cursor.SetCursor(settings.ScrollE.Texture, settings.ScrollE.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollN:
                UnityEngine.Cursor.SetCursor(settings.ScrollN.Texture, settings.ScrollN.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollNE:
                UnityEngine.Cursor.SetCursor(settings.ScrollNE.Texture, settings.ScrollNE.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollNS:
                UnityEngine.Cursor.SetCursor(settings.ScrollNS.Texture, settings.ScrollNS.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollNW:
                UnityEngine.Cursor.SetCursor(settings.ScrollNW.Texture, settings.ScrollNW.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollS:
                UnityEngine.Cursor.SetCursor(settings.ScrollS.Texture, settings.ScrollS.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollSE:
                UnityEngine.Cursor.SetCursor(settings.ScrollSE.Texture, settings.ScrollSE.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollSW:
                UnityEngine.Cursor.SetCursor(settings.ScrollSW.Texture, settings.ScrollSW.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollW:
                UnityEngine.Cursor.SetCursor(settings.ScrollW.Texture, settings.ScrollW.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.ScrollWE:
                UnityEngine.Cursor.SetCursor(settings.ScrollWE.Texture, settings.ScrollWE.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.SizeAll:
                UnityEngine.Cursor.SetCursor(settings.SizeAll.Texture, settings.SizeAll.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.SizeNESW:
                UnityEngine.Cursor.SetCursor(settings.SizeNESW.Texture, settings.SizeNESW.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.SizeNS:
                UnityEngine.Cursor.SetCursor(settings.SizeNS.Texture, settings.SizeNS.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.SizeNWSE:
                UnityEngine.Cursor.SetCursor(settings.SizeNWSE.Texture, settings.SizeNWSE.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.SizeWE:
                UnityEngine.Cursor.SetCursor(settings.SizeWE.Texture, settings.SizeWE.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.UpArrow:
                UnityEngine.Cursor.SetCursor(settings.UpArrow.Texture, settings.UpArrow.HotSpot, CursorMode.Auto);
                break;
            case Noesis.Cursor.Wait:
                UnityEngine.Cursor.SetCursor(settings.Wait.Texture, settings.Wait.HotSpot, CursorMode.Auto);
                break;
        }
    }
    #endregion

    #region Open URL
    private static void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    #endregion

    #region Play Sound
    private static void PlaySound(string filename, float volume)
    {
        if (Application.isPlaying)
        {
            AudioProvider.instance.PlayAudio(filename, volume);
        }
    }
    #endregion

    #region Imports
    [DllImport(Library.Name)]
    static extern void Noesis_RegisterUnityLogCallbacks(UnityLogCallback logCallback,
        UnityVerbosityCallback verbosityCallback);

    [DllImport(Library.Name)]
    static extern bool Noesis_HasFamily(System.IntPtr stream, string family);

    #endregion
}