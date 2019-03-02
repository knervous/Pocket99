using UnityEngine;
using System.IO;


public class NoesisXaml: ScriptableObject
{
    void OnEnable()
    {
        RegisterDependencies();
    }

    void OnDisable()
    {
        UnregisterDependencies();
    }

    public bool CanLoad()
    {
        return NoesisSettings.IsNoesisEnabled() && !string.IsNullOrEmpty(source) && content != null;
    }

    public object Load()
    {
        if (!CanLoad())
        {
            throw new System.Exception("Unexpected empty XAML. Please reimport again");
        }

        RegisterDependencies();
        return Noesis.GUI.LoadXaml(source);
    }

    public void LoadComponent(object component)
    {
        if (!CanLoad())
        {
            throw new System.Exception("Unexpected empty XAML. Please reimport again");
        }

        RegisterDependencies();
        Noesis.GUI.LoadComponent(component, source);
    }

    public void RegisterDependencies()
    {
        if (!_registered && CanLoad())
        {
            NoesisUnity.Init();
            _RegisterDependencies();
            _registered = true;
        }
    }

    public void UnregisterDependencies()
    {
        if (_registered)
        {
            _UnregisterDependencies();
            _registered = false;
        }
    }

    private void _RegisterDependencies()
    {
        NoesisXamlProvider.instance.Register(this);

        if (textures != null)
        {
            foreach (var texture in textures)
            {
                if (texture.uri != null && texture.texture != null)
                {
                    NoesisTextureProvider.instance.Register(texture.uri, texture.texture);
                }
            }
        }

        if (fonts != null)
        {
            foreach (var font in fonts)
            {
                if (font != null)
                {
                    NoesisFontProvider.instance.Register(font);
                }
            }
        }

        if (audios != null)
        {
            foreach (var audio in audios)
            {
                if (audio.uri != null && audio.audio != null)
                {
                    AudioProvider.instance.Register(audio.uri, audio.audio);
                }
            }
        }

        if (xamls != null)
        {
            foreach (var xaml in xamls)
            {
                if (xaml != null)
                {
                    xaml.RegisterDependencies();
                }
            }
        }
    }

    private void _UnregisterDependencies()
    {
        NoesisXamlProvider.instance.Unregister(this);

        if (textures != null)
        {
            foreach (var texture in textures)
            {
                if (texture.uri != null)
                {
                    NoesisTextureProvider.instance.Unregister(texture.uri);
                }
            }
        }

        if (fonts != null)
        {
            foreach (var font in fonts)
            {
                if (font != null)
                {
                    NoesisFontProvider.instance.Unregister(font);
                }
            }
        }

        if (audios != null)
        {
            foreach (var audio in audios)
            {
                if (audio.uri != null)
                {
                    AudioProvider.instance.Unregister(audio.uri);
                }
            }
        }

        if (xamls != null)
        {
            foreach (var xaml in xamls)
            {
                if (xaml != null)
                {
                    xaml.UnregisterDependencies();
                }
            }
        }
    }

    public string source;
    public byte[] content;

    [System.Serializable]
    public struct Texture
    {
        public string uri;
        public UnityEngine.Texture texture;
    }

    [System.Serializable]
    public struct Audio
    {
        public string uri;
        public UnityEngine.AudioClip audio;
    }

    public NoesisXaml[] xamls;
    public NoesisFont[] fonts;
    public Texture[] textures;
    public Audio[] audios;

    private bool _registered = false;
}
