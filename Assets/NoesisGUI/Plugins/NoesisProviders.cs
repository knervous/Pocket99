using Noesis;
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
        lock(_lock)
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
    }

    public void Unregister(string uri)
    {
        lock(_lock)
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
    }

    public override void GetTextureInfo(string uri, ref uint width, ref uint height)
    {
        width = 0;
        height = 0;

        Value v;

        lock(_lock)
        {
            _textures.TryGetValue(uri, out v);
        }

        // Mutex must not be locked here beacuse GetNativeTexturePtr() may wait for the render thread
        if (v.texture != null)
        {
            UnityEngine.Texture2D texture2D = v.texture as UnityEngine.Texture2D;

            width = (uint)v.texture.width;
            height = (uint)v.texture.height;

            v.width = v.texture.width;
            v.height = v.texture.height;
            v.numLevels = texture2D ? texture2D.mipmapCount : 1;
            v.nativePtr = v.texture.GetNativeTexturePtr();

            lock(_lock)
            {
                _textures[uri] = v;
            }
        }
    }

    public override Noesis.Texture LoadTexture(string uri)
    {
        lock(_lock)
        {
            Value v;
            if (_textures.TryGetValue(uri, out v))
            {
                if (v.nativePtr != System.IntPtr.Zero)
                {
                    return Noesis.Texture.WrapTexture(null, v.nativePtr, v.width, v.height, v.numLevels);
                }
            }

            return null;
        }
    }

    public struct Value
    {
        public int refs;
        public int width;
        public int height;
        public int numLevels;
        public System.IntPtr nativePtr;
        public UnityEngine.Texture texture;
    }

    private Dictionary<string, Value> _textures;
    private readonly object _lock = new object();
}

/// <summary>
/// Font provider
/// </summary>
public class NoesisFontProvider: FontProvider
{
    public static NoesisFontProvider instance = new NoesisFontProvider();

    NoesisFontProvider()
    {
        _fonts = new Dictionary<string, Value>();
    }

    public void Register(NoesisFont font)
    {
        string uri = font.source;
        Value v;

        if (_fonts.TryGetValue(uri, out v))
        {
            v.refs++;
            v.font = font;
            _fonts[uri] = v;
        }
        else
        {
            _fonts[uri] = new Value() { refs = 1, font = font };
        }

        string folder = System.IO.Path.GetDirectoryName(uri);
        string filename = System.IO.Path.GetFileName(uri);
        RegisterFont(folder, filename);
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

    public override Stream OpenFont(string folder, string id)
    {
        Value v;
        _fonts.TryGetValue(folder + "/" + id, out v);

        if (v.font != null && v.font.content != null)
        {
            return new MemoryStream(v.font.content);
        }

        return null;
    }

    public struct Value
    {
        public int refs;
        public NoesisFont font;
    }

    private Dictionary<string, Value> _fonts;
}