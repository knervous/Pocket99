using UnityEngine;
using UnityEditor;
using Noesis;

[CustomEditor(typeof(NoesisXaml))]
public class NoesisXamlEditor: Editor
{
    private Noesis.View _viewPreview;
    private Noesis.View _viewPreviewGUI;
    private string _error;
    private UnityEngine.Rendering.CommandBuffer _commands;

    private bool DeviceIsD3D()
    {
        return SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Direct3D9 ||
            SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Direct3D11 ||
            SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Direct3D12;
    }

    private void CreatePreviewView()
    {
        try
        {
            _error = null;
            NoesisXaml xaml = (NoesisXaml)target;
            FrameworkElement root = xaml.Load() as FrameworkElement;
            View.RenderFlags flags = DeviceIsD3D() ? View.RenderFlags.FlipY : 0;

            if (root != null)
            {
                _viewPreview = Noesis.GUI.CreateView(root);
                _viewPreview.SetFlags(flags);
                NoesisRenderer.RegisterView(_viewPreview);
            }
        }
        catch (System.Exception e)
        {
            _error = e.Message;
            UnityEngine.Debug.LogException(e);
        }
    }

    private void CreatePreviewGUIView()
    {
        try
        {
            NoesisXaml xaml = (NoesisXaml)target;
            FrameworkElement root = xaml.Load() as FrameworkElement;
            View.RenderFlags flags = DeviceIsD3D() ? View.RenderFlags.FlipY : 0;

            if (root != null)
            {
                _viewPreviewGUI = Noesis.GUI.CreateView(root);
                _viewPreviewGUI.SetFlags(flags);
                NoesisRenderer.RegisterView(_viewPreviewGUI);
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
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
            NoesisRenderer.Shutdown(_viewPreview);
        }

        if (_viewPreviewGUI != null)
        {
            NoesisRenderer.Shutdown(_viewPreviewGUI);
        }
    }

    public override void OnInspectorGUI()
    {
        NoesisXaml xaml = (NoesisXaml)target;

        EditorGUILayout.LabelField("XAML Dependencies", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        EditorGUILayout.Foldout(true, "Textures", false);
        if (xaml.textures != null)
        {
            EditorGUILayout.BeginHorizontal();
            foreach (var texture in xaml.textures)
            {
                EditorGUILayout.ObjectField(texture, typeof(Texture2D), false);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Foldout(true, "Fonts", false);
        if (xaml.fonts != null)
        {
            EditorGUILayout.BeginHorizontal();
            foreach (var font in xaml.fonts)
            {
                EditorGUILayout.ObjectField(font, typeof(NoesisFont), false);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Foldout(true, "Resource Dictionaries", false);
        if (xaml.xamls != null)
        {
            EditorGUILayout.BeginHorizontal();
            foreach (var xaml_ in xaml.xamls)
            {
                EditorGUILayout.ObjectField(xaml_, typeof(NoesisXaml), false);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.Space();

        if (!string.IsNullOrEmpty(_error))
        {
            EditorGUILayout.HelpBox(_error, MessageType.Error);
        }
    }

    private bool CanRender()
    {
        NoesisXaml xaml = (NoesisXaml)target;
        return xaml != null && xaml.CanLoad() && NoesisSettings.Get().previewEnabled &&
            !UnityEditorInternal.InternalEditorUtility.inBatchMode;
    }

    public override bool HasPreviewGUI()
    {
        return CanRender();
    }

    public override void DrawPreview(UnityEngine.Rect r)
    {
        if (CanRender())
        {
            if (r.width > 4 && r.height > 4)
            {
                if (_viewPreview == null)
                {
                    CreatePreviewView();
                }

                if (_viewPreview != null)
                {
                    Graphics.DrawTexture(r, RenderPreview(_viewPreview, (int)r.width, (int)r.height));
                }
            }
        }
    }

    public override void OnPreviewGUI(UnityEngine.Rect r, GUIStyle background)
    {
        if (CanRender())
        {
            if (_viewPreviewGUI == null)
            {
                CreatePreviewGUIView();
            }

            if (_viewPreviewGUI != null)
            {
                UnityEngine.GUI.DrawTexture(r, RenderPreview(_viewPreviewGUI, (int)r.width * 2, (int)r.height * 2));
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
            View.RenderFlags flags = DeviceIsD3D() ? View.RenderFlags.FlipY : 0;

            if (_renderMode == RenderMode.Normal)
            {
                _viewPreview.SetFlags(flags);
            }
            else if (_renderMode == RenderMode.Wireframe)
            {
                _viewPreview.SetFlags(flags | View.RenderFlags.Wireframe);
            }
            else if (_renderMode == RenderMode.Batches)
            {
                _viewPreview.SetFlags(flags | View.RenderFlags.ColorBatches);
            }
            else if (_renderMode == RenderMode.Overdraw)
            {
                _viewPreview.SetFlags(flags | View.RenderFlags.Overdraw);
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

            if (_viewPreviewGUI != null)
            {
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
            if (CanRender() && view != null)
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
