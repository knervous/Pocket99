using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Noesis;
using System.IO;
using System.Collections.Generic;

[CustomEditor(typeof(NoesisFont))]
public class NoesisFontEditor: Editor
{
    struct Face
    {   
        public int index;
        public string family;
        public Noesis.FontWeight weight;
        public Noesis.FontStyle style;
        public Noesis.FontStretch stretch;
    }

    private List<Face> _faces = new List<Face>();
    private int _index;
    private Noesis.View _view;
    private Noesis.View _viewIcon;
    private UnityEngine.Rendering.CommandBuffer _commands;

    private bool IsGL()
    {
        return
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore;
    }

    private void AddRun(TextBlock text, int size, FontFamily family)
    {
        text.Inlines.Add(new Run(size.ToString() + " "));

        Run run = new Run("The quick brown fox jumps over the lazy dog. 1234567890");
        run.FontSize = size;
        run.FontFamily = family;
        run.FontWeight = _faces[_index].weight;
        run.FontStyle = _faces[_index].style;
        run.FontStretch = _faces[_index].stretch;
        text.Inlines.Add(run);

        text.Inlines.Add(new LineBreak());
    }

    private FrameworkElement GetRoot()
    {
        StackPanel root = new StackPanel();
        root.Background = new SolidColorBrush(Colors.White);

        if (_faces.Count > 0 && target != null)
        {
            _index = System.Math.Min(_index, _faces.Count - 1);
            NoesisFont font = (NoesisFont)target;
            FontFamily family = new FontFamily(System.IO.Path.GetDirectoryName(font.source) + "/#" + _faces[_index].family);

            TextBlock text = new TextBlock();
            text.Margin = new Thickness(2);
            text.Foreground = new SolidColorBrush(Colors.Black);
 
            AddRun(text, 12, family);
            AddRun(text, 18, family);
            AddRun(text, 24, family);
            AddRun(text, 36, family);
            AddRun(text, 48, family);
            AddRun(text, 60, family);
            AddRun(text, 72, family);

            root.Children.Add(text);
        }

        return root;
    }

    private FrameworkElement GetRootIcon()
    {
        Noesis.Grid root = new Noesis.Grid();
        root.Background = new SolidColorBrush(Colors.White);

        Viewbox box = new Viewbox();
        box.Margin = new Thickness(10);

        if (_faces.Count > 0 && target != null)
        {
            NoesisFont font = (NoesisFont)target;

            TextBlock text = new TextBlock();
            text.Foreground = new SolidColorBrush(Colors.Black);
            text.FontFamily = new FontFamily(System.IO.Path.GetDirectoryName(font.source) + "/#" + _faces[0].family);
            text.FontWeight = _faces[0].weight;
            text.FontStyle = _faces[0].style;
            text.FontStretch = _faces[0].stretch;
            text.Text = "Abg";

            box.Child = text;
        }
        
        root.Children.Add(box);
        return root;
    }

    private void CreateView()
    {
        try
        {
            FrameworkElement root = GetRoot();
            _view = Noesis.GUI.CreateView(root);
            _view.SetFlags(IsGL() ? 0 : RenderFlags.FlipY);

            _commands.Clear();
            NoesisRenderer.RegisterView(_view, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }

    private void CreateViewIcon()
    {
        try
        {
            FrameworkElement root = GetRootIcon();
            _viewIcon = Noesis.GUI.CreateView(root);
            _viewIcon.SetFlags(IsGL() ? 0 : RenderFlags.FlipY);

            _commands.Clear();
            NoesisRenderer.RegisterView(_viewIcon, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }

    public void RegisterFont()
    {
        if (target != null)
        {
            NoesisFont font = (NoesisFont)target;
            if (font.source != null)
            {
                NoesisUnity.Init();

                using (MemoryStream stream = new MemoryStream(font.content))
                {
                    _index = 0;
                    _faces.Clear();
                    Noesis.GUI.EnumFontFaces(stream, (index_, family_, weight_, style_, stretch_) =>
                    {
                        _faces.Add(new Face() { index = index_, family = family_, weight = weight_, style = style_, stretch = stretch_ } );
                    });
                }

                if (_faces.Count > 0)
                {
                    NoesisFontProvider.instance.Register(font);
                }
            }
        }
    }

    public void UnregisterFont()
    {
        if (target != null && _faces.Count > 0)
        {
            NoesisFont font = (NoesisFont)target;
            if (font.source != null)
            {
                NoesisFontProvider.instance.Unregister(font);
            }
        }

        _faces.Clear();
    }

    public void OnEnable()
    {
        if (_commands == null)
        {
            _commands = new UnityEngine.Rendering.CommandBuffer();
        }

        if (NoesisSettings.IsNoesisEnabled())
        {
            RegisterFont();
        }
    }

    public void OnDisable()
    {
        if (_view != null)
        {
            _commands.Clear();
            NoesisRenderer.UnregisterView(_view, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }

        if (_viewIcon != null)
        {
            _commands.Clear();
            NoesisRenderer.UnregisterView(_viewIcon, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }

        if (NoesisSettings.IsNoesisEnabled())
        {
            UnregisterFont();
        }
    }

    public override void OnInspectorGUI()
    {
        foreach (var face in _faces)
        {
            EditorGUILayout.LabelField("Face " + face.index, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Family", face.family, EditorStyles.textField);
            EditorGUILayout.LabelField("Weight", face.weight.ToString(), EditorStyles.textField);
            EditorGUILayout.LabelField("Style", face.style.ToString(), EditorStyles.textField);
            EditorGUILayout.LabelField("Stretch", face.stretch.ToString(), EditorStyles.textField);
        }
    }

    private bool CanRender()
    {
        return NoesisSettings.IsNoesisEnabled() && NoesisSettings.Get().previewEnabled && _faces.Count > 0;
    }

    public override bool HasPreviewGUI()
    {
        if (_view == null)
        {
            CreateView();
        }

        if (_view == null || _view.Content == null)
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
                    if (_view != null && _view.Content != null)
                    {
                        UnityEngine.GUI.DrawTexture(r, RenderPreview(_view, (int)r.width, (int)r.height));
                    }
                }
            }
        }
    }

    public override void OnPreviewSettings()
    {
        string[] options = new string[_faces.Count];
        int[] values = new int[_faces.Count];

        for (int i = 0; i < _faces.Count; i++)
        {
            options[i] = "Face " + i;
            values[i] = i;
        }

        int index = EditorGUILayout.IntPopup(_index, options, values);
        if (index != _index && _view != null)
        {
            _commands.Clear();
            NoesisRenderer.UnregisterView(_view, _commands);
            Graphics.ExecuteCommandBuffer(_commands);

            _index = index;
            CreateView();
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        if (CanRender())
        {
            if (_viewIcon == null)
            {
                CreateViewIcon();
            }

            if (_viewIcon != null && _viewIcon.Content != null)
            {
                if (NoesisSettings.Get().debugImporter)
                {
                    Debug.Log("â†” RenderStaticPreview " + assetPath);
                }

                RenderTexture rt = RenderPreview(_viewIcon, width, height);

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
