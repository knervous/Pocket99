using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoesisView))]
public class NoesisViewEditor : Editor
{
    enum RenderMode
    {
        Normal,
        Wireframe,
        Batches,
        Overdraw
    };

    private RenderMode ToRenderMode(Noesis.View.RenderFlags renderFlags)
    {
        if ((renderFlags & Noesis.View.RenderFlags.Wireframe) == Noesis.View.RenderFlags.Wireframe)
        {
            return RenderMode.Wireframe;
        }
        else if ((renderFlags & Noesis.View.RenderFlags.ColorBatches) == Noesis.View.RenderFlags.ColorBatches)
        {
            return RenderMode.Batches;
        }
        else if ((renderFlags & Noesis.View.RenderFlags.Overdraw) == Noesis.View.RenderFlags.Overdraw)
        {
            return RenderMode.Overdraw;
        }
        else
        {
            return RenderMode.Normal;
        }
    }

    private Noesis.View.RenderFlags ToRenderFlags(RenderMode renderMode)
    {
        if (renderMode == RenderMode.Wireframe)
        {
            return Noesis.View.RenderFlags.Wireframe;
        }
        else if (renderMode == RenderMode.Batches)
        {
            return Noesis.View.RenderFlags.ColorBatches;
        }
        else if (renderMode == RenderMode.Overdraw)
        {
            return Noesis.View.RenderFlags.Overdraw;
        }

        return 0;
    }

    public override void OnInspectorGUI()
    {
        NoesisView view = target as NoesisView;

        // Register changes in the component so scene can be saved, and Undo is also enabled
        Undo.RecordObject(view, "Noesis View");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(new GUIContent("Render Mode",
            "When attached to a camera the interface is rendered as a camera overlay. " +
            "When attached to a Renderer or UI.RawImage the interface is rendered to texture"));
        if (view.IsRenderToTexture())
        {
            GUILayout.Label("Render Texture", "AssetLabel");
        }
        else
        {
            GUILayout.Label("Camera Overlay", "AssetLabel");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        view._xaml = (NoesisXaml)EditorGUILayout.ObjectField(
            new GUIContent("XAML", "Drop here a xaml file that defines the user interface"),
            view._xaml, typeof(NoesisXaml), false);

        EditorGUILayout.Space();

        view._antiAliasingMode = (Noesis.View.AntialiasingMode)EditorGUILayout.EnumPopup(
            new GUIContent("Antialiasing Mode",
            "Antialiasing Mode: MSAA=Uses hardware multisample, PPA=Propietary GPU accelerated antialiasing algorithm"),
            view._antiAliasingMode);

        view._tessellationQuality = (Noesis.View.TessellationQuality)EditorGUILayout.EnumPopup(
            new GUIContent("Tessellation Quality",
            "Specifies tessellation quality"), view._tessellationQuality);

        EditorGUILayout.Space();

        RenderMode renderMode = ToRenderMode(view._renderFlags);
        renderMode = (RenderMode)EditorGUILayout.EnumPopup(new GUIContent("Render Flags", ""), renderMode);
        view._renderFlags = ToRenderFlags(renderMode);

        EditorGUILayout.Space();

        view._enableKeyboard = EditorGUILayout.Toggle(new GUIContent("Enable Keyboard",
            "When enabled, Keyboard input events are processed by NoesisGUI view"),
            view._enableKeyboard);

        view._enableMouse = EditorGUILayout.Toggle(new GUIContent("Enable Mouse",
            "When enabled, Mouse input events are processed by NoesisGUI view"),
            view._enableMouse);

        view._enableTouch = EditorGUILayout.Toggle(new GUIContent("Enable Touch",
            "When enabled, Touch input events are processed by NoesisGUI view"),
            view._enableTouch);

        view._emulateTouch = EditorGUILayout.Toggle(new GUIContent("Emulate Touch",
            "When enabled, Touch input events are emulated by using the Mouse"),
            view._emulateTouch);

        view._useRealTimeClock = EditorGUILayout.Toggle(new GUIContent("Real Time Clock",
            "When enabled, Time.realtimeSinceStartup is used instead of Time.time for animations"),
            view._useRealTimeClock);
    }
}
