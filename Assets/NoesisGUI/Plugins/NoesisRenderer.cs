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
    public static void RegisterView(Noesis.View view)
    {
        Noesis_RegisterView(view.CPtr, view.GetHashCode());
    }

    /// <summary>
    /// Sends offscreen render commands to native code
    /// </summary>
    public static void RenderOffscreen(Noesis.View view, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEvent(_renderOffscreenCallback, view.GetHashCode());
    }

    /// <summary>
    /// Sends offscreen render commands to native code
    /// </summary>
    public static void RenderOffscreen(Noesis.View view)
    {
        UnityEngine.GL.IssuePluginEvent(_renderOffscreenCallback, view.GetHashCode());
    }

    /// <summary>
    /// Sends render commands to native code
    /// </summary>
    public static void RenderOnscreen(Noesis.View view, bool flipY, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEvent(flipY ? _renderOnscreenFlipYCallback : _renderOnscreenCallback, view.GetHashCode());
    }

    /// <summary>
    /// Sends render commands to native code
    /// </summary>
    public static void RenderOnscreen(Noesis.View view, bool flipY)
    {
        UnityEngine.GL.IssuePluginEvent(flipY ? _renderOnscreenFlipYCallback : _renderOnscreenCallback, view.GetHashCode());
    }

    /// <summary>
    /// Unregister given renderer
    /// </summary>
    public static void Shutdown(Noesis.View view, UnityEngine.Rendering.CommandBuffer commands)
    {
        commands.IssuePluginEvent(_renderShutdownCallback, view.GetHashCode());
    }

    /// <summary>
    /// Unregister given renderer
    /// </summary>
    public static void Shutdown(Noesis.View view)
    {
        UnityEngine.GL.IssuePluginEvent(_renderShutdownCallback, view.GetHashCode());
    }

    #region Private
    [DllImport(Noesis.Library.Name)]
    private static extern void Noesis_RegisterView(HandleRef renderer, int id);

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderOffscreenCallback();

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderOnscreenCallback();

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderOnscreenFlipYCallback();

    [DllImport(Noesis.Library.Name)]
    private static extern System.IntPtr Noesis_GetRenderShutdownCallback();

    private static System.IntPtr _renderOffscreenCallback = Noesis_GetRenderOffscreenCallback();
    private static System.IntPtr _renderOnscreenCallback = Noesis_GetRenderOnscreenCallback();
    private static System.IntPtr _renderOnscreenFlipYCallback = Noesis_GetRenderOnscreenFlipYCallback();
    private static System.IntPtr _renderShutdownCallback = Noesis_GetRenderShutdownCallback();
    #endregion
}