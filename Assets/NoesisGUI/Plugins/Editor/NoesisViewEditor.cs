using System;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoesisView))]
public class NoesisViewEditor : Editor
{
    enum RenderMode
    {
        None,
        Wireframe,
        Batches,
        Overdraw
    };

    private RenderMode ToRenderMode(Noesis.RenderFlags renderFlags)
    {
        if ((renderFlags & Noesis.RenderFlags.Wireframe) == Noesis.RenderFlags.Wireframe)
        {
            return RenderMode.Wireframe;
        }
        else if ((renderFlags & Noesis.RenderFlags.ColorBatches) == Noesis.RenderFlags.ColorBatches)
        {
            return RenderMode.Batches;
        }
        else if ((renderFlags & Noesis.RenderFlags.Overdraw) == Noesis.RenderFlags.Overdraw)
        {
            return RenderMode.Overdraw;
        }
        else
        {
            return RenderMode.None;
        }
    }

    private Noesis.RenderFlags ToRenderFlags(RenderMode renderMode)
    {
        if (renderMode == RenderMode.Wireframe)
        {
            return Noesis.RenderFlags.Wireframe;
        }
        else if (renderMode == RenderMode.Batches)
        {
            return Noesis.RenderFlags.ColorBatches;
        }
        else if (renderMode == RenderMode.Overdraw)
        {
            return Noesis.RenderFlags.Overdraw;
        }

        return 0;
    }

    public override void OnInspectorGUI()
    {
        NoesisView view = target as NoesisView;

        // Register changes in the component so scene can be saved, and Undo is also enabled
        Undo.RecordObject(view, "Noesis View");

        EditorGUILayout.LabelField(new GUIContent("Render Mode", "Views attached to camera objects work in 'Camera Overlay' mode. 'Render Texture' mode is enabled in all other cases"),
            new GUIContent(view.IsRenderToTexture() ? "Render Texture" : "Camera Overlay"), EditorStyles.popup);

        if (view.IsRenderToTexture())
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(new GUIContent("Target Texture", "The texture to render this View into"));
            view._texture = (RenderTexture)EditorGUILayout.ObjectField(view._texture, typeof(RenderTexture), false);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        view._xaml = (NoesisXaml)EditorGUILayout.ObjectField(new GUIContent("XAML", "User interface definition XAML"), view._xaml, typeof(NoesisXaml), false);

        EditorGUILayout.BeginHorizontal();
        GUIContent[] options = { new GUIContent("Low Quality"), new GUIContent("Medium Quality"), new GUIContent("High Quality"), new GUIContent("Custom Quality") };
        int[] values = { 0, 1, 2, 3};
        int value = view._tessellationMaxPixelError == 0.7f ? 0 : view._tessellationMaxPixelError == 0.4f ? 1 : view._tessellationMaxPixelError == 0.2f ? 2 : 3;
        value = EditorGUILayout.IntPopup(new GUIContent("Tessellation Pixel Error", "Tessellation curve tolerance in screen space. " + 
            "'Medium Quality' is usually fine for PPAA (non-multisampled) while 'High Quality' is the recommended pixel error if you are rendering to a 8x multisampled surface"),
            value, options, values);
        float maxError = value == 0 ? 0.7f : value == 1 ? 0.4f : value == 2 ? 0.2f: view._tessellationMaxPixelError;
        view._tessellationMaxPixelError = Math.Max(0.01f, EditorGUILayout.FloatField(maxError, GUILayout.Width(64)));
        EditorGUILayout.EndHorizontal();

        RenderMode renderMode = ToRenderMode(view._renderFlags);
        renderMode = (RenderMode)EditorGUILayout.EnumPopup(new GUIContent("Debug Render Flags",
            "Enables debugging render flags. No debug flags are active by default"), renderMode);
        view._renderFlags = ToRenderFlags(renderMode);

        view._isPPAAEnabled = EditorGUILayout.Toggle(new GUIContent("Enable PPAA", "PPAA is a 'cheap' antialiasing algorithm useful when GPU MSAA is not enabled"), view._isPPAAEnabled);
        view._continuousRendering = EditorGUILayout.Toggle(new GUIContent("Continuous Rendering", "When continuous rendering is disabled rendering only happens when needed." +
            " For performance purposes and to save battery this is the default mode when rendering to texture.\n\nThis flag is ignored in 'Camera Overlay' mode and instead the property " +
            "NoesisView.NeedsRendering must be used with a manually repainted camera."), view._continuousRendering);
        EditorGUILayout.Space();

        view._enableKeyboard = EditorGUILayout.Toggle(new GUIContent("Enable Keyboard",
            "If Keyboard input events are processed by this view"),
            view._enableKeyboard);

        view._enableMouse = EditorGUILayout.Toggle(new GUIContent("Enable Mouse",
            "If Mouse input events are processed by this view"),
            view._enableMouse);

        view._enableTouch = EditorGUILayout.Toggle(new GUIContent("Enable Touch",
            "If Touch input events are processed by this view"),
            view._enableTouch);

        view._emulateTouch = EditorGUILayout.Toggle(new GUIContent("Emulate Touch",
            "If Touch input events are emulated by the Mouse"),
            view._emulateTouch);

        view._useRealTimeClock = EditorGUILayout.Toggle(new GUIContent("Real Time Clock",
            "If Time.realtimeSinceStartup is used instead of Time.time for animations"),
            view._useRealTimeClock);
    }

    public override bool HasPreviewGUI()
    {
        return Application.isPlaying;
    }

    private GUIStyle m_PreviewLabelStyle;

    protected GUIStyle previewLabelStyle
    {
        get
        {
            if (m_PreviewLabelStyle == null)
            {
                m_PreviewLabelStyle = new GUIStyle("PreOverlayLabel")
                {
                    richText = true,
                    alignment = TextAnchor.UpperLeft,
                    fontStyle = FontStyle.Normal
                };
            }

            return m_PreviewLabelStyle;
        }
    }

    public override bool RequiresConstantRepaint()
    {
        return Application.isPlaying;
    }

    public override void OnPreviewGUI(Rect rect, GUIStyle background)
    {
        NoesisView view = target as NoesisView;
        Noesis.ViewStats stats = view.GetStats();

        StringBuilder str = new StringBuilder();
        str.AppendLine("<b>FrameTime : </b>" + String.Format("{0:F2}", stats.FrameTime) + " ms");
        str.AppendLine("<b>UpdateTime: </b>" + String.Format("{0:F2}", stats.UpdateTime) + " ms");
        str.AppendLine("<b>RenderTime: </b>" + String.Format("{0:F2}", stats.RenderTime) + " ms");
        str.AppendLine();
        str.AppendLine("<b>Triangles: </b>" + stats.Triangles);
        str.AppendLine("<b>Draws: </b>" + stats.Draws);
        str.AppendLine("<b>Batches: </b>" + stats.Batches);
        str.AppendLine();
        str.AppendLine("<b>Tessellations: </b>" + stats.Tessellations);
        str.AppendLine("<b>Flushes: </b>" + stats.Flushes);
        str.AppendLine("<b>GeometrySize: </b>" + stats.GeometrySize);
        str.AppendLine();
        str.AppendLine("<b>Masks: </b>" + stats.Masks);
        str.AppendLine("<b>Opacities: </b>" + stats.Opacities);
        str.AppendLine("<b>RenderTargetSwitches: </b>" + stats.RenderTargetSwitches);
        str.AppendLine();
        str.AppendLine("<b>UploadedRamps: </b>" + stats.UploadedRamps);
        str.AppendLine("<b>RasterizedGlyphs: </b>" + stats.RasterizedGlyphs);
        str.AppendLine("<b>DiscardedGlyphTiles: </b>" + stats.DiscardedGlyphTiles);

        GUI.Label(rect, str.ToString(), previewLabelStyle);
    }
}
