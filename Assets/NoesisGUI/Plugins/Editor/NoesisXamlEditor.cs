using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using Noesis;

[CustomEditor(typeof(NoesisXaml))]
public class NoesisXamlEditor: Editor
{
    private Noesis.View _viewPreview;
    private Noesis.View _viewPreviewGUI;
    private UnityEngine.Rendering.CommandBuffer _commands;

    private bool IsGL()
    {
        return
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore;
    }

    private void CreatePreviewView()
    {
        try
        {
            NoesisXaml xaml = (NoesisXaml)target;
            FrameworkElement root = xaml.Load() as FrameworkElement;
            _viewPreview = Noesis.GUI.CreateView(root);
            _viewPreview.SetFlags(IsGL() ? 0 : RenderFlags.FlipY);

            _commands.Clear();
            NoesisRenderer.RegisterView(_viewPreview, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }

    private void CreatePreviewGUIView()
    {
        // Avoid logging parse errors twice by muting them when generating thumbnails
        NoesisUnity.MuteLog();

        try
        {
            NoesisXaml xaml = (NoesisXaml)target;
            FrameworkElement root = xaml.Load() as FrameworkElement;
            _viewPreviewGUI = Noesis.GUI.CreateView(root);
            _viewPreviewGUI.SetFlags(IsGL() ? 0 : RenderFlags.FlipY);

            _commands.Clear();
            NoesisRenderer.RegisterView(_viewPreviewGUI, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }

        NoesisUnity.UnmuteLog();
    }

    public void OnEnable()
    {
        if (_commands == null)
        {
            _commands = new UnityEngine.Rendering.CommandBuffer();
        }
    }

    public void OnDisable()
    {
        if (_viewPreview != null)
        {
            _commands.Clear();
            NoesisRenderer.UnregisterView(_viewPreview, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }

        if (_viewPreviewGUI != null)
        {
            _commands.Clear();
            NoesisRenderer.UnregisterView(_viewPreviewGUI, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }
    }

    private bool _showTextures = true;
    private bool _showFonts = true;
    private bool _showAudios = true;
    private bool _showXAMLs = true;

    public override void OnInspectorGUI()
    {
        NoesisXaml xaml = (NoesisXaml)target;

        EditorGUILayout.LabelField("XAML Dependencies", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        _showTextures = EditorGUILayout.Foldout(_showTextures, "Textures", false);
        if (_showTextures && xaml.textures != null)
        {
            foreach (var texture in xaml.textures)
            {
                EditorGUILayout.ObjectField(texture.texture, typeof(Texture2D), false);
            }
        }

        _showFonts = EditorGUILayout.Foldout(_showFonts, "Fonts", false);
        if (_showFonts && xaml.fonts != null)
        {
            foreach (var font in xaml.fonts)
            {
                EditorGUILayout.ObjectField(font, typeof(NoesisFont), false);
            }
        }

        _showAudios = EditorGUILayout.Foldout(_showAudios, "Audios", false);
        if (_showAudios && xaml.audios != null)
        {
            foreach (var audio_ in xaml.audios)
            {
                EditorGUILayout.ObjectField(audio_.audio, typeof(AudioClip), false);
            }
        }

        _showXAMLs = EditorGUILayout.Foldout(_showXAMLs, "XAMLs", false);
        if (_showXAMLs && xaml.xamls != null)
        {
            foreach (var xaml_ in xaml.xamls)
            {
                EditorGUILayout.ObjectField(xaml_, typeof(NoesisXaml), false);
            }
        }

        EditorGUI.indentLevel--;
    }

    private bool CanRender()
    {
        NoesisXaml xaml = (NoesisXaml)target;
        return NoesisSettings.IsNoesisEnabled() && xaml != null && xaml.CanLoad() && NoesisSettings.Get().previewEnabled;
    }

    public override bool HasPreviewGUI()
    {
        if (_viewPreview == null)
        {
            CreatePreviewView();
        }

        if (_viewPreview == null || _viewPreview.Content == null)
        {
            return false;
        }

        return CanRender();
    }

    public override void OnPreviewGUI(UnityEngine.Rect r, GUIStyle background)
    {
        if (Event.current.type == EventType.Repaint)
        {
            if (CanRender())
            {
                if (r.width > 4 && r.height > 4)
                {
                    if (_viewPreview != null && _viewPreview.Content != null)
                    {
                        UnityEngine.GUI.DrawTexture(r, RenderPreview(_viewPreview, (int)r.width, (int)r.height));
                    }
                }
            }
        }
    }

    private enum RenderMode
    {
        Normal,
        Wireframe,
        Batches,
        Overdraw
    }

    private RenderMode _renderMode = RenderMode.Normal;

    public override void OnPreviewSettings()
    {
        _renderMode = (RenderMode)EditorGUILayout.EnumPopup(_renderMode);

        if (_viewPreview != null)
        {
            RenderFlags flags = IsGL() ? 0 : RenderFlags.FlipY;

            if (_renderMode == RenderMode.Normal)
            {
                _viewPreview.SetFlags(flags);
            }
            else if (_renderMode == RenderMode.Wireframe)
            {
                _viewPreview.SetFlags(flags | RenderFlags.Wireframe);
            }
            else if (_renderMode == RenderMode.Batches)
            {
                _viewPreview.SetFlags(flags | RenderFlags.ColorBatches);
            }
            else if (_renderMode == RenderMode.Overdraw)
            {
                _viewPreview.SetFlags(flags | RenderFlags.Overdraw);
            }
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        if (CanRender())
        {
            if (_viewPreviewGUI == null)
            {
                CreatePreviewGUIView();
            }

            if (_viewPreviewGUI != null && _viewPreviewGUI.Content != null)
            {
                if (NoesisSettings.Get().debugImporter)
                {
                    Debug.Log("↔ RenderStaticPreview " + assetPath);
                }

                RenderTexture rt = RenderPreview(_viewPreviewGUI, width, height);

                if (rt != null)
                {
                    RenderTexture prev = RenderTexture.active;
                    RenderTexture.active = rt;

                    Texture2D tex = new Texture2D(width, height);
                    tex.ReadPixels(new UnityEngine.Rect(0, 0, width, height), 0, 0);
                    tex.Apply(true);

                    RenderTexture.active = prev;
                    return tex;
                }
            }
        }

        return null;
    }

    private RenderTexture RenderPreview(Noesis.View view, int width, int height)
    {
        try
        {
            if (CanRender() && view != null && view.Content != null)
            {
                view.SetSize(width, height);
                view.Update(0.0f);

                RenderTexture rt = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 8);
  
                _commands.Clear();
                NoesisRenderer.RenderOffscreen(view, _commands);
                _commands.SetRenderTarget(rt);
                _commands.ClearRenderTarget(true, true, UnityEngine.Color.clear, 0.0f);
                NoesisRenderer.RenderOnscreen(view, false, _commands);
                Graphics.ExecuteCommandBuffer(_commands);

                GL.InvalidateState();

                RenderTexture.ReleaseTemporary(rt);

                return rt;
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }

        return null;
    }
}
