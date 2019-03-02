using Noesis;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

/// <summary>
/// Xaml provider
/// </summary>
public class NoesisXamlProvider: XamlProvider
{
    public static NoesisXamlProvider instance = new NoesisXamlProvider();

    NoesisXamlProvider()
    {
        _xamls = new Dictionary<string, Value>();
    }

    public void Register(NoesisXaml xaml)
    {
        string uri = xaml.source;
        Value v;

        if (_xamls.TryGetValue(uri, out v))
        {
            v.refs++;
            v.xaml = xaml;
            _xamls[uri] = v;
        }
        else
        {
            _xamls[uri] = new Value() { refs = 1, xaml = xaml };
        }
    }

    public void Unregister(NoesisXaml xaml)
    {
        string uri = xaml.source;
        Value v;

        if (_xamls.TryGetValue(uri, out v))
        {
            if (v.refs == 1)
            {
                _xamls.Remove(xaml.source);
            }
            else
            {
                v.refs--;
                _xamls[uri] = v;
            }
        }
    }

    public override Stream LoadXaml(string uri)
    {
        Value v;
        if (_xamls.TryGetValue(uri, out v))
        {
            return new MemoryStream(v.xaml.content);
        }

        return null;
    }

    public struct Value
    {
        public int refs;
        public NoesisXaml xaml;
    }

    private Dictionary<string, Value> _xamls;
}

/// <summary>
/// Audio provider
/// </summary>
public class AudioProvider
{
    public static AudioProvider instance = new AudioProvider();

    AudioProvider()
    {
        _audios = new Dictionary<string, Value>();
    }

    public void Register(string uri, UnityEngine.AudioClip audio)
    {
        Value v;
        if (_audios.TryGetValue(uri, out v))
        {
            v.refs++;
            v.audio = audio;
            _audios[uri] = v;
        }
        else
        {
            _audios[uri] = new Value() { refs = 1, audio = audio };
        }
    }

    public void Unregister(string uri)
    {
        Value v;
        if (_audios.TryGetValue(uri, out v))
        {
            if (v.refs == 1)
            {
                _audios.Remove(uri);
            }
            else
            {
                v.refs--;
                _audios[uri] = v;
            }
        }
    }

    public void PlayAudio(string uri, float volume)
    {
        Value v;
        if (_audios.TryGetValue(uri, out v) && v.audio != null)
        {
            UnityEngine.AudioSource.PlayClipAtPoint(v.audio, UnityEngine.Vector3.zero, volume);
        }
        else
        {
            UnityEngine.Debug.LogError("AudioClip not found '" + uri + "'");
        }
    }

    public struct Value
    {
        public int refs;
        public UnityEngine.AudioClip audio;
    }

    private Dictionary<string, Value> _audios;
}

/// <summary>
/// Texture provider
/// </summary>
public class NoesisTextureProvider: TextureProvider
{
    public static NoesisTextureProvider instance = new NoesisTextureProvider();

    NoesisTextureProvider()
    {
        _textures = new Dictionary<string, Value>();
    }

    public void Register(string uri, UnityEngine.Texture texture)
    {
        Value v;
        if (_textures.TryGetValue(uri, out v))
        {
            v.refs++;
            v.texture = texture;
            _textures[uri] = v;
        }
        else
        {
            _textures[uri] = new Value() { refs = 1, texture = texture };
        }
    }

    public void Unregister(string uri)
    {
        Value v;
        if (_textures.TryGetValue(uri, out v))
        {
            if (v.refs == 1)
            {
                _textures.Remove(uri);
            }
            else
            {
                v.refs--;
                _textures[uri] = v;
            }
        }
    }

    public override void GetTextureInfo(string uri, out uint width_, out uint height_)
    {
        int width = 0;
        int height = 0;
        int numLevels = 0;
        System.IntPtr nativePtr = IntPtr.Zero;

        Value v;
        if (_textures.TryGetValue(uri, out v))
        {
            if (v.texture != null)
            {
                width = v.texture.width;
                height = v.texture.height;
                numLevels = v.texture is UnityEngine.Texture2D ? ((UnityEngine.Texture2D)v.texture).mipmapCount : 1;
                nativePtr = v.texture.GetNativeTexturePtr();
            }
        }

        // Send to C++
        Noesis_TextureProviderStoreTextureInfo(swigCPtr.Handle, uri, width, height, numLevels, nativePtr);

        width_ = (uint)width;
        height_ = (uint)height;
    }

    public struct Value
    {
        public int refs;
        public UnityEngine.Texture texture;
    }

    private Dictionary<string, Value> _textures;

    internal new static IntPtr Extend(string typeName)
    {
        return Noesis_TextureProviderExtend(System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(typeName));
    }

    #region Imports
    [DllImport(Library.Name)]
    static extern IntPtr Noesis_TextureProviderExtend(IntPtr typeName);

    [DllImport(Library.Name)]
    static extern void Noesis_TextureProviderStoreTextureInfo(IntPtr cPtr,
        [MarshalAs(UnmanagedType.LPStr)] string filename, int width, int height, int numLevels,
        IntPtr nativePtr);
    #endregion
}

/// <summary>
/// Font provider
/// </summary>
public class NoesisFontProvider: FontProvider
{
    private static LockFontCallback _lockFont = LockFont;
    private static UnlockFontCallback _unlockFont = UnlockFont;
    public static NoesisFontProvider instance = new NoesisFontProvider();

    NoesisFontProvider()
    {
        Noesis_FontProviderSetLockUnlockCallbacks(_lockFont, _unlockFont);
        _fonts = new Dictionary<string, Value>();
    }

    public void Register(NoesisFont font)
    {
        bool register = false;

        string uri = font.source;
        Value v;

        if (_fonts.TryGetValue(uri, out v))
        {
            register = v.font != font;

            v.refs++;
            v.font = font;
            _fonts[uri] = v;
        }
        else
        {
            register = true;
            _fonts[uri] = new Value() { refs = 1, font = font };
        }

        if (register)
        {
            string folder = System.IO.Path.GetDirectoryName(uri);
            string filename = System.IO.Path.GetFileName(uri);
            RegisterFont(folder, filename);
        }
    }

    public void Unregister(NoesisFont font)
    {
        string uri = font.source;
        Value v;

        if (_fonts.TryGetValue(uri, out v))
        {
            if (v.refs == 1)
            {
                _fonts.Remove(uri);
            }
            else
            {
                v.refs--;
                _fonts[uri] = v;
            }
        }
    }

    private delegate void LockFontCallback(string folder, string filename, out IntPtr handle, out IntPtr addr, out int length);
    [MonoPInvokeCallback(typeof(LockFontCallback))]
    private static void LockFont(string folder, string filename, out IntPtr handle, out IntPtr addr, out int length)
    {
        NoesisFontProvider provider = NoesisFontProvider.instance;

        Value v;
        provider._fonts.TryGetValue(folder + "/" + filename, out v);

        if (v.font != null && v.font.content != null)
        {
            GCHandle h = GCHandle.Alloc(v.font.content, GCHandleType.Pinned);
            handle =  GCHandle.ToIntPtr(h);
            addr = h.AddrOfPinnedObject();
            length = v.font.content.Length;
            return;
        }

        handle = IntPtr.Zero;
        addr = IntPtr.Zero;
        length = 0;
    }

    private delegate void UnlockFontCallback(IntPtr handle);
    [MonoPInvokeCallback(typeof(UnlockFontCallback))]
    private static void UnlockFont(IntPtr handle)
    {
        GCHandle h = GCHandle.FromIntPtr(handle);
        h.Free();
    }

    public struct Value
    {
        public int refs;
        public NoesisFont font;
    }

    private Dictionary<string, Value> _fonts;

    internal new static IntPtr Extend(string typeName)
    {
        return Noesis_FontProviderExtend(System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(typeName));
    }

    #region Imports
    [DllImport(Library.Name)]
    static extern IntPtr Noesis_FontProviderExtend(IntPtr typeName);

    [DllImport(Library.Name)]
    static extern void Noesis_FontProviderSetLockUnlockCallbacks(LockFontCallback lockFont, UnlockFontCallback unlockFont);
    #endregion
}