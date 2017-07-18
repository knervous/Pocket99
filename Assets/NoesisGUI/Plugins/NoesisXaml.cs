using UnityEngine;
using System.IO;


[PreferBinarySerialization]
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
        return !string.IsNullOrEmpty(source) && content != null;
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
        if (CanLoad())
        {
            throw new System.Exception("Unexpected empty XAML. Please reimport again");
        }

        RegisterDependencies();
        Noesis.GUI.LoadComponent(component, source);
    }

    public void ReloadDependencies()
    {
        UnregisterDependencies();
        RegisterDependencies();
    }

    private void RegisterDependencies()
    {
        if (!_registered && CanLoad())
        {
            NoesisUnity.Init();
            _RegisterDependencies();
            _registered = true;
        }
    }

    private void UnregisterDependencies()
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

        if (textures != null && texturePaths != null)
        {
            for (int i = 0; i < texturePaths.Length; i++)
            {
                NoesisTextureProvider.instance.Register(texturePaths[i], textures[i]);
            }
        }

        if (fonts != null)
        {
            foreach (var font in fonts)
            {
                NoesisFontProvider.instance.Register(font);
            }
        }

        if (xamls != null)
        {
            foreach (var xaml in xamls)
            {
                xaml.RegisterDependencies();
            }
        }
    }

    private void _UnregisterDependencies()
    {
        NoesisXamlProvider.instance.Unregister(this);

        if (texturePaths != null)
        {
            foreach (var texture in texturePaths)
            {
                NoesisTextureProvider.instance.Unregister(texture);
            }
        }

        if (fonts != null)
        {
            foreach (var font in fonts)
            {
                NoesisFontProvider.instance.Unregister(font);
            }
        }

        if (xamls != null)
        {
            foreach (var xaml in xamls)
            {
                xaml.UnregisterDependencies();
            }
        }
    }

    public string source;
    public byte[] content;

    public NoesisFont[] fonts;
    public string[] texturePaths;
    public UnityEngine.Texture[] textures;
    public NoesisXaml[] xamls;

    private bool _registered = false;
}
