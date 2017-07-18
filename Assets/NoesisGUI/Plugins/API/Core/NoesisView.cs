using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class View
    {
        #region Public properties
        /// <summary>
        /// Returns the renderer, to be used in the render thread.
        /// </summary>
        public Renderer Renderer
        {
            get
            {
                return _renderer;
            }
        }

        /// <summary>
        /// Returns the root element.
        /// </summary>
        public FrameworkElement Content
        {
            get
            {
                return _content;
            }
        }

        #endregion

        #region Configuration
        /// <summary>
        /// Sets the size of the surface where UI elements will layout and render.
        /// </summary>
        /// <param name="width">Surface width in pixels.</param>
        /// <param name="height">Surface height in pixels.</param>
        public void SetSize(int width, int height)
        {
            Noesis_View_SetSize_(CPtr, width, height);
        }

        /// <summary>
        /// Inidicates the antialiasing method used for rendering the vectors.
        /// </summary>
        public enum AntialiasingMode
        {
            /// <summary>
            /// Indicates that the antialising algorithm that will be used rely on the multisampling
            /// that is active in the target surface, if any.
            /// </summary>
            MSAA,

            /// <summary>
            /// Indicates that besides the multisampling in the target surface a per-primitive
            /// algorithm will be used. PPAA implements antialiasing by extruding the contours of
            /// the triangles smoothing them.
            /// </summary>
            PPAA
        }

        /// <summary>
        /// Sets antialiasing mode. MSAA is the default.
        /// </summary>
        /// <param name="mode"></param>
        public void SetAntialiasingMode(AntialiasingMode mode)
        {
            Noesis_View_SetAntialiasingMode_(CPtr, (int)mode);
        }

        /// <summary>
        /// Indicates the tessellation quality applied to vector curves.
        /// </summary>
        public enum TessellationQuality
        {
            /// The lowest tessellation quality for curves.
            Low,

            /// Medium tessellation quality for curves.
            Medium,

            /// The highest tessellation quality for curves.
            High
        }

        /// <summary>
        /// Sets tessellation quality. Medium is the default.
        /// </summary>
        public void SetTessellationQuality(TessellationQuality quality)
        {
            Noesis_View_SetTessellationQuality_(CPtr, (int)quality);
        }

        /// <summary>
        /// Flags to debug UI render.
        /// </summary>
        [Flags]
        public enum RenderFlags
        {
            /// <summary>Toggles wireframe mode when rendering triangles.</summary>
            Wireframe = 1,

            /// <summary>Each batch submitted to the GPU is given a unique solid color.</summary>
            ColorBatches = 2,

            /// <summary>
            /// Display pixel overdraw using blending layers. Different colors are used for each
            /// type of triangle. Green for normal, Red for opacities and Blue for clipping masks.
            /// </summary>
            Overdraw = 4,

            /// <summary>Inverts the render vertically.</summary>
            FlipY = 8,
        };

        /// <summary>
        /// Enables debugging flags. No debug flags are active by default
        /// </summary>
        public void SetFlags(RenderFlags flags)
        {
            Noesis_View_SetFlags_(CPtr, (int)flags);
        }
        #endregion

        #region Input management
        /// <summary>
        /// Resets mouse, touch and keyboard internal state.
        /// </summary>
        public void ResetInputState()
        {
            Noesis_View_ResetInputState_(CPtr);
        }

        #region Mouse input events
        /// <summary>
        /// Notifies the View that mouse was moved. The mouse position is specified in renderer
        /// surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        public void MouseMove(int x, int y)
        {
            Noesis_View_MouseMove_(CPtr, x, y);
        }

        /// <summary>
        /// Notifies the View that a mouse button was pressed. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="button">Indicates which button was pressed.</param>
        public void MouseDown(int x, int y, Noesis.MouseButton button)
        {
            Noesis_View_MouseDown_(CPtr, x, y, (int)button);
        }

        /// <summary>
        /// Notifies the View that a mouse button was released. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="button">Indicates which button was released.</param>
        public void MouseUp(int x, int y, Noesis.MouseButton button)
        {
            Noesis_View_MouseUp_(CPtr, x, y, (int)button);
        }

        /// <summary>
        /// Notifies the View of a mouse button double click. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="button">Indicates which button was pressed.</param>
        public void MouseDoubleClick(int x, int y, Noesis.MouseButton button)
        {
            Noesis_View_MouseDoubleClick_(CPtr, x, y, (int)button);
        }

        /// <summary>
        /// Notifies the View that mouse wheel was rotated. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="wheelRotation">Indicates the amount mouse wheel has changed.</param>
        public void MouseWheel(int x, int y, int wheelRotation)
        {
            Noesis_View_MouseWheel_(CPtr, x, y, (int)wheelRotation);
        }
        #endregion

        #region Touch input events
        /// <summary>
        /// Notifies the View that a finger is moving on the screen. The finger position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Finger x-coordinate.</param>
        /// <param name="y">Finger y-coordinate.</param>
        /// <param name="touchId">Finger identifier.</param>
        public void TouchMove(int x, int y, uint touchId)
        {
            Noesis_View_TouchMove_(CPtr, x, y, touchId);
        }

        /// <summary>
        /// Notifies the View that a finger touches the screen. The finger position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Finger x-coordinate.</param>
        /// <param name="y">Finger y-coordinate.</param>
        /// <param name="touchId">Finger identifier.</param>
        public void TouchDown(int x, int y, uint touchId)
        {
            Noesis_View_TouchDown_(CPtr, x, y, touchId);
        }

        /// <summary>
        /// Notifies the View that a finger is raised off the screen. The finger position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Finger x-coordinate.</param>
        /// <param name="y">Finger y-coordinate.</param>
        /// <param name="touchId">Finger identifier.</param>
        public void TouchUp(int x, int y, uint touchId)
        {
            Noesis_View_TouchUp_(CPtr, x, y, touchId);
        }
        #endregion

        #region Keyboard input events
        /// <summary>
        /// Notifies the View that a key was pressed.
        /// </summary>
        /// <param name="key">Key identifier.</param>
        public void KeyDown(Noesis.Key key)
        {
            Noesis_View_KeyDown_(CPtr, (int)key);
        }

        /// <summary>
        /// Notifies the View that a key was released.
        /// </summary>
        /// <param name="key">Key identifier.</param>
        public void KeyUp(Noesis.Key key)
        {
            Noesis_View_KeyUp_(CPtr, (int)key);
        }

        /// <summary>
        /// Notifies Renderer that a key was translated to the corresponding character.
        /// </summary>
        /// <param name="ch">Unicode character value.</param>
        public void Char(uint ch)
        {
            Noesis_View_Char_(CPtr, ch);
        }
        #endregion
        #endregion

        #region Render process
        /// <summary>
        /// Performs a layout pass and sends updates to the render tree.
        /// </summary>
        /// <param name="timeInSeconds">Time elapsed since the start of the application.</param>
        public void Update(double timeInSeconds)
        {
            Extend.Update();
            GUI.SoftwareKeyboard.Update();
            Noesis_View_Update_(CPtr, timeInSeconds);
        }

        // TODO: Rendering event
        #endregion

        #region Private members
        internal View(FrameworkElement content)
        {
            _view = new BaseComponent(Noesis_View_Create_(BaseComponent.getCPtr(content)), true);
            _renderer = new Renderer(this);
            _content = content;
        }

        internal HandleRef CPtr { get { return BaseComponent.getCPtr(_view); } }

        BaseComponent _view;
        Renderer _renderer;
        FrameworkElement _content;
        #endregion

        #region Imports
        static IntPtr Noesis_View_Create_(HandleRef content)
        {
            IntPtr result = Noesis_View_Create(content);
            Error.Check();
            return result;
        }

        static void Noesis_View_SetSize_(HandleRef view, int width, int height)
        {
            Noesis_View_SetSize(view, width, height);
            Error.Check();
        }

        static void Noesis_View_SetAntialiasingMode_(HandleRef view, int aaMode)
        {
            Noesis_View_SetAntialiasingMode(view, aaMode);
            Error.Check();
        }

        static void Noesis_View_SetTessellationQuality_(HandleRef view, int tessQuality)
        {
            Noesis_View_SetTessellationQuality(view, tessQuality);
            Error.Check();
        }

        static void Noesis_View_SetFlags_(HandleRef view, int flags)
        {
            Noesis_View_SetFlags(view, flags);
            Error.Check();
        }

        static void Noesis_View_ResetInputState_(HandleRef view)
        {
            Noesis_View_ResetInputState(view);
            Error.Check();
        }

        static void Noesis_View_MouseMove_(HandleRef view, int x, int y)
        {
            Noesis_View_MouseMove(view, x, y);
            Error.Check();
        }

        static void Noesis_View_MouseDown_(HandleRef view, int x, int y, int button)
        {
            Noesis_View_MouseDown(view, x, y, button);
            Error.Check();
        }

        static void Noesis_View_MouseUp_(HandleRef view, int x, int y, int button)
        {
            Noesis_View_MouseUp(view, x, y, button);
            Error.Check();
        }

        static void Noesis_View_MouseDoubleClick_(HandleRef view, int x, int y, int button)
        {
            Noesis_View_MouseDoubleClick(view, x, y, button);
            Error.Check();
        }

        static void Noesis_View_MouseWheel_(HandleRef view, int x, int y, int wheelRotation)
        {
            Noesis_View_MouseWheel(view, x, y, wheelRotation);
            Error.Check();
        }

        static void Noesis_View_TouchMove_(HandleRef view, int x, int y, uint touchId)
        {
            Noesis_View_TouchMove(view, x, y, touchId);
            Error.Check();
        }

        static void Noesis_View_TouchDown_(HandleRef view, int x, int y, uint touchId)
        {
            Noesis_View_TouchDown(view, x, y, touchId);
            Error.Check();
        }

        static void Noesis_View_TouchUp_(HandleRef view, int x, int y, uint touchId)
        {
            Noesis_View_TouchUp(view, x, y, touchId);
            Error.Check();
        }

        static void Noesis_View_KeyDown_(HandleRef view, int key)
        {
            Noesis_View_KeyDown(view, key);
            Error.Check();
        }

        static void Noesis_View_KeyUp_(HandleRef view, int key)
        {
            Noesis_View_KeyUp(view, key);
            Error.Check();
        }

        static void Noesis_View_Char_(HandleRef view, uint ch)
        {
            Noesis_View_Char(view, ch);
            Error.Check();
        }

        static void Noesis_View_Update_(HandleRef view, double timeInSeconds)
        {
            Noesis_View_Update(view, timeInSeconds);
            Error.Check();
        }

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_View_Create(HandleRef contenttheme);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetSize(HandleRef view, int width, int height);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetAntialiasingMode(HandleRef view, int aaMode);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetTessellationQuality(HandleRef view, int tessQuality);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetFlags(HandleRef view, int flags);

        [DllImport(Library.Name)]
        static extern void Noesis_View_ResetInputState(HandleRef view);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseMove(HandleRef view, int x, int y);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseDown(HandleRef view, int x, int y, int button);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseUp(HandleRef view, int x, int y, int button);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseDoubleClick(HandleRef view, int x, int y, int button);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseWheel(HandleRef view, int x, int y, int wheelRotation);

        [DllImport(Library.Name)]
        static extern void Noesis_View_TouchMove(HandleRef view, int x, int y, uint touchId);

        [DllImport(Library.Name)]
        static extern void Noesis_View_TouchDown(HandleRef view, int x, int y, uint touchId);

        [DllImport(Library.Name)]
        static extern void Noesis_View_TouchUp(HandleRef view, int x, int y, uint touchId);

        [DllImport(Library.Name)]
        static extern void Noesis_View_KeyDown(HandleRef view, int key);

        [DllImport(Library.Name)]
        static extern void Noesis_View_KeyUp(HandleRef view, int key);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Char(HandleRef view, uint ch);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Update(HandleRef view, double timeInSeconds);
        #endregion
    }
}
