using System.Runtime.InteropServices;

/// <summary>
/// In Unity, the render thread is only accesible in C++ using IssuePluginEvent(). This is a helper
/// class to communicate a C# view with its C++ renderer.
/// </summary>
public class NoesisRenderer
{
    /// <summary>
    /// Registers a view in the render thread
    /// </summary>
    public static void RegisterView(Noesis.View view, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEventAndData(_renderRegisterCallback, 0, view.CPtr.Handle);
    }

    /// <summary>
    /// Sends offscreen render commands to native code
    /// </summary>
    public static void RenderOffscreen(Noesis.View view, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEventAndData(_renderOffscreenCallback, 0, view.CPtr.Handle);
    }

    /// <summary>
    /// Sends render commands to native code
    /// </summary>
    public static void RenderOnscreen(Noesis.View view, bool flipY, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEventAndData(_renderOnscreenCallback, flipY ? 1 : 0, view.CPtr.Handle);
    }

    /// <summary>
    /// Unregister given renderer
    /// </summary>
    public static void UnregisterView(Noesis.View view, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEventAndData(_renderUnregisterCallback, 0, view.CPtr.Handle);
    }

    #region Private
    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderRegisterCallback();

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderOffscreenCallback();

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderOnscreenCallback();

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderUnregisterCallback();

    private static System.IntPtr _renderRegisterCallback = Noesis_GetRenderRegisterCallback();
    private static System.IntPtr _renderOffscreenCallback = Noesis_GetRenderOffscreenCallback();
    private static System.IntPtr _renderOnscreenCallback = Noesis_GetRenderOnscreenCallback();
    private static System.IntPtr _renderUnregisterCallback = Noesis_GetRenderUnregisterCallback();
    #endregion
}