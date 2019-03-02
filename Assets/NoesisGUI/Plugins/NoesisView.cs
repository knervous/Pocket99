using UnityEngine;
using Noesis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
[AddComponentMenu("NoesisGUI/Noesis View")]
[HelpURL("https://www.noesisengine.com/docs")]
[DisallowMultipleComponent]
public class NoesisView: MonoBehaviour
{
    #region Public properties

    /// <summary>
    /// User interface definition XAML
    /// </summary>
    public NoesisXaml Xaml
    {
        set { this._xaml = value; }
        get { return this._xaml; }
    }

    /// <summary>
    /// The texture to render this View into
    /// </summary>
    public RenderTexture Texture
    {
        set { this._texture = value; }
        get { return this._texture; }
    }

    /// <summary>
    /// PPAA is a 'cheap' antialiasing algorithm useful when GPU MSAA is not enabled
    /// </summary>
    public bool IsPPAAEnabled
    {
        set { this._isPPAAEnabled = value; }
        get { return this._isPPAAEnabled; }
    }

    /// <summary>
    /// Tessellation curve tolerance in screen space. 'Medium Quality' is usually fine for PPAA (non-multisampled) 
    /// while 'High Quality' is the recommended pixel error if you are rendering to a 8x multisampled surface
    /// </summary>
    public float TessellationMaxPixelError
    {
        set { this._tessellationMaxPixelError = value; }
        get { return this._tessellationMaxPixelError; }
    }

    /// <summary>
    /// Bit flags used for debug rendering purposes.
    /// </summary>
    public RenderFlags RenderFlags
    {
        set { this._renderFlags = value; }
        get { return this._renderFlags; }
    }

    /// <summary>
    /// When continuous rendering is disabled rendering only happens when needed. For performance
    /// purposes and to save battery this is the default mode when rendering to texture.
    /// </summary>
    public bool ContinuousRendering
    {
        set { this._continuousRendering = value; }
        get { return this._continuousRendering; }
    }

    /// <summary>
    /// After LateUpdate() has been invoked this flag indicates if the GUI needs to be repainted.
    /// This flag can be used on manually painted cameras to optimize performance and save battery.
    /// </summary>
    public bool NeedsRendering
    {
        set { this._needsRendering = value; }
        get { return this._needsRendering; }
    }

    /// <summary>
    /// Enables keyboard input management.
    /// </summary>
    public bool EnableKeyboard
    {
        set { this._enableKeyboard = value; }
        get { return this._enableKeyboard; }
    }

    /// <summary>
    /// Enables mouse input management.
    /// </summary>
    public bool EnableMouse
    {
        set { this._enableMouse = value; }
        get { return this._enableMouse; }
    }

    /// <summary>
    /// Enables touch input management.
    /// </summary>
    public bool EnableTouch
    {
        set { this._enableTouch = value; }
        get { return this._enableTouch; }
    }

    /// <summary>
    /// Emulate touch input with mouse.
    /// </summary>
    public bool EmulateTouch
    {
        set { this._emulateTouch = value; }
        get { return this._emulateTouch; }
    }

    /// <summary>
    /// When enabled, UI is updated using Time.realtimeSinceStartup.
    /// </summary>
    public bool UseRealTimeClock
    {
        set { this._useRealTimeClock = value; }
        get { return this._useRealTimeClock; }
    }

    /// <summary>
    /// Gets the root of the loaded Xaml.
    /// </summary>
    /// <returns>Root element.</returns>
    public FrameworkElement Content
    {
        get { return _uiView != null ? _uiView.Content : null; }
    }

    /// <summary>
    /// Indicates if this component is rendering UI to a RenderTexture.
    /// </summary>
    /// <returns></returns>
    public bool IsRenderToTexture()
    {
        return _textureCamera != null || gameObject.GetComponent<Camera>() == null;
    }

    #endregion

    #region Public events

    #region Render
    public event RenderingEventHandler Rendering
    {
        add
        {
            if (_uiView != null)
            {
                _uiView.Rendering += value;
            }
        }
        remove
        {
            if (_uiView != null)
            {
                _uiView.Rendering -= value;
            }
        }
    }

    public ViewStats GetStats()
    {
        if (_uiView != null)
        {
            return _uiView.GetStats();
        }

        return new ViewStats();
    }
    #endregion

    #region Keyboard input events
    /// <summary>
    /// Notifies Renderer that a key was pressed.
    /// </summary>
    /// <param name="key">Key identifier.</param>
    public bool KeyDown(Noesis.Key key)
    {
        if (_uiView != null)
        {
            return _uiView.KeyDown(key);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer that a key was released.
    /// </summary>
    /// <param name="key">Key identifier.</param>
    public bool KeyUp(Noesis.Key key)
    {
        if (_uiView != null)
        {
            return _uiView.KeyUp(key);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer that a key was translated to the corresponding character.
    /// </summary>
    /// <param name="ch">Unicode character value.</param>
    public bool Char(uint ch)
    {
        if (_uiView != null)
        {
            return _uiView.Char(ch);
        }

        return false;
    }
    #endregion

    #region Mouse input events
    /// <summary>
    /// Notifies Renderer that mouse was moved. The mouse position is specified in renderer
    /// surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    public bool MouseMove(int x, int y)
    {
        if (_uiView != null)
        {
            return _uiView.MouseMove(x, y);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer that a mouse button was pressed. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="button">Indicates which button was pressed.</param>
    public bool MouseButtonDown(int x, int y, Noesis.MouseButton button)
    {
        if (_uiView != null)
        {
            return _uiView.MouseButtonDown(x, y, button);
        }

        return false;
    }

    /// Notifies Renderer that a mouse button was released. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="button">Indicates which button was released.</param>
    public bool MouseButtonUp(int x, int y, Noesis.MouseButton button)
    {
        if (_uiView != null)
        {
            return _uiView.MouseButtonUp(x, y, button);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer of a mouse button double click. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="button">Indicates which button was pressed.</param>
    public bool MouseDoubleClick(int x, int y, Noesis.MouseButton button)
    {
        if (_uiView != null)
        {
            return _uiView.MouseDoubleClick(x, y, button);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer that mouse wheel was rotated. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="wheelRotation">Indicates the amount mouse wheel has changed.</param>
    public bool MouseWheel(int x, int y, int wheelRotation)
    {
        if (_uiView != null)
        {
            return _uiView.MouseWheel(x, y, wheelRotation);
        }

        return false;
    }
    #endregion

    #region Touch input events
    /// <summary>
    /// Notifies Renderer that a finger is moving on the screen. The finger position is
    /// specified in renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Finger x-coordinate.</param>
    /// <param name="y">Finger y-coordinate.</param>
    /// <param name="touchId">Finger identifier.</param>
    public bool TouchMove(int x, int y, uint touchId)
    {
        if (_uiView != null)
        {
            return _uiView.TouchMove(x, y, touchId);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer that a finger touches the screen. The finger position is
    /// specified in renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Finger x-coordinate.</param>
    /// <param name="y">Finger y-coordinate.</param>
    /// <param name="touchId">Finger identifier.</param>
    public bool TouchDown(int x, int y, uint touchId)
    {
        if (_uiView != null)
        {
            return _uiView.TouchDown(x, y, touchId);
        }

        return false;
    }

    /// <summary>
    /// Notifies Renderer that a finger is raised off the screen. The finger position is
    /// specified in renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Finger x-coordinate.</param>
    /// <param name="y">Finger y-coordinate.</param>
    /// <param name="touchId">Finger identifier.</param>
    public bool TouchUp(int x, int y, uint touchId)
    {
        if (_uiView != null)
        {
            return _uiView.TouchUp(x, y, touchId);
        }

        return false;
    }
    #endregion

    #endregion

    #region Public methods

    /// <summary>
    /// Loads the user interface specified in the XAML property
    /// </summary>
    public void LoadXaml(bool force)
    {
        if (force)
        {
            DestroyView();
        }

        if (_xaml != null && _uiView == null)
        {
            FrameworkElement content = _xaml.Load() as FrameworkElement;

            if (content == null)
            {
                throw new System.Exception("XAML root is not FrameworkElement");
            }

            CreateView(content);
        }
    }

    #endregion

    #region Private members

    #region MonoBehavior component messages

    /// <summary>
    /// Called once when component is attached to GameObject for the first time
    /// </summary>
    void Reset()
    {
        _isPPAAEnabled = true;
        _tessellationMaxPixelError = Noesis.TessellationMaxPixelError.MediumQuality.Error;
        _renderFlags = 0;
        _continuousRendering = gameObject.GetComponent<Camera>() != null;
        _enableKeyboard = true;
        _enableMouse = true;
        _enableTouch = true;
        _emulateTouch = false;
        _useRealTimeClock = false;
    }

    void Start()
    {
        // Disabling this lets you skip the GUI layout phase
        useGUILayout = false;
    }

    void OnEnable()
    {
        _commands = new UnityEngine.Rendering.CommandBuffer();

        if (gameObject.GetComponent<Camera>() == null)
        {
            if (_texture != null &&  _textureCamera == null)
            {
                _textureCamera = gameObject.AddComponent<Camera>();
                _textureCamera.clearFlags = CameraClearFlags.SolidColor;
                _textureCamera.backgroundColor = new UnityEngine.Color(0.0f, 0.0f, 0.0f, 0.0f);
                _textureCamera.renderingPath = RenderingPath.Forward;
                _textureCamera.depthTextureMode = DepthTextureMode.None;
                _textureCamera.opaqueSortMode = UnityEngine.Rendering.OpaqueSortMode.NoDistanceSort;
                _textureCamera.transparencySortMode = TransparencySortMode.Orthographic;
                _textureCamera.clearStencilAfterLightingPass = false;
#if UNITY_5_6_OR_NEWER
                _textureCamera.allowHDR = false;
#else
                _textureCamera.hdr = false;
#endif
                _textureCamera.useOcclusionCulling = false;
                _textureCamera.cullingMask = 0;
                _textureCamera.targetTexture = _texture;
                _textureCamera.enabled = false;
            }
        }

        LoadXaml(false);
    }

    void OnDisable()
    {
    }

    void OnDestroy()
    {
        if (_textureCamera != null)
        {
            UnityEngine.Object.Destroy(_textureCamera);
            _textureCamera = null;
        }

        DestroyView();
    }

    UnityEngine.EventSystems.PointerEventData _pointerData;

    private UnityEngine.Vector2 ProjectPointer(float x, float y)
    {
        if (_textureCamera == null)
        {
            return new UnityEngine.Vector2(x, UnityEngine.Screen.height - y);
        }
        else if (_texture != null)
        {
            // Project using texture coordinates

            // First try with Unity GUI RawImage objects
            UnityEngine.EventSystems.EventSystem eventSystem = UnityEngine.EventSystems.EventSystem.current;

            if (eventSystem != null && eventSystem.IsPointerOverGameObject())
            {
                UnityEngine.Vector2 pos = new UnityEngine.Vector2(x, y);

                if (_pointerData == null)
                {
                    _pointerData = new UnityEngine.EventSystems.PointerEventData(eventSystem)
                    {
                        pointerId = 0,
                        position = pos
                    };
                }
                else
                {
                    _pointerData.Reset();
                }

                _pointerData.delta = pos - _pointerData.position;
                _pointerData.position = pos;

                UnityEngine.RectTransform rect = GetComponent<UnityEngine.RectTransform>();

                if (rect != null &&
                    UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        rect, _pointerData.position, _pointerData.pressEventCamera, out pos))
                {
                    UnityEngine.Vector2 pivot = new UnityEngine.Vector2(
                        rect.pivot.x * rect.rect.width,
                        rect.pivot.y * rect.rect.height);

                    float texCoordX = (pos.x + pivot.x) / rect.rect.width;
                    float texCoordY = (pos.y + pivot.y) / rect.rect.height;

                    float localX = _texture.width * texCoordX;
                    float localY = _texture.height * (1.0f - texCoordY);
                    return new UnityEngine.Vector2(localX, localY);
                }
            }

            // NOTE: A MeshCollider must be attached to the target to obtain valid
            // texture coordinates, otherwise Hit Testing won't work

            UnityEngine.Ray ray = UnityEngine.Camera.main.ScreenPointToRay(new UnityEngine.Vector3(x, y, 0));

            UnityEngine.RaycastHit hit;
            if (UnityEngine.Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    float localX = _texture.width * hit.textureCoord.x;
                    float localY = _texture.height * (1.0f - hit.textureCoord.y);
                    return new UnityEngine.Vector2(localX, localY);
                }
            }

            return new UnityEngine.Vector2(-1, -1);
        }

        return Vector2.zero;
    }

    private bool _touchEmulated = false;
    private UnityEngine.Vector3 _mousePos;

    private void UpdateMouse()
    {
        // mouse move
        if (_mousePos != UnityEngine.Input.mousePosition)
        {
            _mousePos = UnityEngine.Input.mousePosition;
            UnityEngine.Vector2 mouse = ProjectPointer(_mousePos.x, _mousePos.y);

            if (_emulateTouch && _touchEmulated)
            {
                _uiView.TouchMove((int)mouse.x, (int)mouse.y, 0);
            }
            else
            {
                _uiView.MouseMove((int)mouse.x, (int)mouse.y);
            }
        }

        try
        {
            // mouse wheel
            // NOTE: Scroll wheel axis produces deltas of 0,1 * Axis Sensitivity (def=1), and our
            // view expects a value of 120 as a tick in the mouse wheel
            int mouseWheel = (int)(UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 1200.0f);
            if (mouseWheel != 0)
            {
                UnityEngine.Vector2 mouse = ProjectPointer(_mousePos.x, _mousePos.y);
                _uiView.MouseWheel((int)mouse.x, (int)mouse.y, mouseWheel);
            }
        }
        catch (Exception) {}
    }

    private void UpdateTouch()
    {
        for (int i = 0; i < UnityEngine.Input.touchCount; i++) 
        {
            UnityEngine.Touch touch = UnityEngine.Input.GetTouch(i);
            uint id = (uint)touch.fingerId;
            UnityEngine.Vector2 pos = ProjectPointer(touch.position.x, touch.position.y);
            UnityEngine.TouchPhase phase = touch.phase;

            if (phase == UnityEngine.TouchPhase.Began)
            {
                _uiView.TouchDown((int)pos.x, (int)pos.y, id);
            }
            else if (phase == UnityEngine.TouchPhase.Moved || phase == UnityEngine.TouchPhase.Stationary)
            {
                _uiView.TouchMove((int)pos.x, (int)pos.y, id);
            }
            else
            {
                _uiView.TouchUp((int)pos.x, (int)pos.y, id);
            }
        }
    }

    [FlagsAttribute] 
    enum VirtualKeys
    {
         Up = 1,
         Down = 2,
         Left = 4,
         Right = 8,
         Accept = 16,
         Cancel = 32,
         Menu = 64,
         View = 128,
         PageUp = 256,
         PageDown = 512,
         PageLeft = 1024,
         PageRight = 2048
    }

    private static readonly Dictionary<VirtualKeys, Noesis.Key> VirtualKeysMap = new Dictionary<VirtualKeys, Noesis.Key>
    {
        { VirtualKeys.Up, Key.GamepadUp },
        { VirtualKeys.Down, Key.GamepadDown },
        { VirtualKeys.Left, Key.GamepadLeft },
        { VirtualKeys.Right, Key.GamepadRight },
        { VirtualKeys.Accept, Key.GamepadAccept },
        { VirtualKeys.Cancel, Key.GamepadCancel },
        { VirtualKeys.Menu, Key.GamepadMenu},
        { VirtualKeys.View, Key.GamepadView },
        { VirtualKeys.PageUp, Key.GamepadPageUp },
        { VirtualKeys.PageDown, Key.GamepadPageDown },
        { VirtualKeys.PageLeft, Key.GamepadPageLeft },
        { VirtualKeys.PageRight, Key.GamepadPageRight },
    };

    private VirtualKeys _virtualKeys = 0;

    private void UpdateVirtualKeys()
    {
        VirtualKeys virtualKeys = 0;

        try
        {
            if (Input.GetAxis("Noesis_Vertical") > 0.5f) virtualKeys |= VirtualKeys.Up;
            if (Input.GetAxis("Noesis_Vertical") < -0.5f) virtualKeys |= VirtualKeys.Down;
        }
        catch (Exception) {}

        try
        {
            if (Input.GetAxis("Noesis_Horizontal") > 0.5f) virtualKeys |= VirtualKeys.Right;
            if (Input.GetAxis("Noesis_Horizontal") < -0.5f) virtualKeys |= VirtualKeys.Left;
        }
        catch (Exception) {}

        try { if (Input.GetButton("Noesis_Accept")) virtualKeys |= VirtualKeys.Accept; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_Cancel")) virtualKeys |= VirtualKeys.Cancel; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_Menu")) virtualKeys |= VirtualKeys.Menu; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_View")) virtualKeys |= VirtualKeys.View; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_PageUp")) virtualKeys |= VirtualKeys.PageUp; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_PageDown")) virtualKeys |= VirtualKeys.PageDown; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_PageLeft")) virtualKeys |= VirtualKeys.PageLeft; } catch (Exception) {}
        try { if (Input.GetButton("Noesis_PageRight")) virtualKeys |= VirtualKeys.PageRight; } catch (Exception) {}

        VirtualKeys delta = virtualKeys ^ _virtualKeys;
        if (delta != 0)
        {
            foreach (var pair in VirtualKeysMap)
            {
                if ((delta & pair.Key) > 0)
                {
                    if ((virtualKeys & pair.Key) > 0)
                    {
                        _uiView.KeyDown(pair.Value);
                    }
                    else
                    {
                        _uiView.KeyUp(pair.Value);
                    }
                }
            }
        }

         _virtualKeys = virtualKeys;
    }

    private void UpdateInputs()
    {
        if (_enableMouse)
        {
            UpdateMouse();
        }

        if (_enableTouch)
        {
            UpdateTouch();
        }

        if (_enableKeyboard)
        {
            UpdateVirtualKeys();
        }
    }

    private void UpdateSettings()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        if (camera != null)
        {
            _uiView.SetSize(camera.pixelWidth, camera.pixelHeight);
        }

        _uiView.SetIsPPAAEnabled(_isPPAAEnabled);
        _uiView.SetTessellationMaxPixelError(_tessellationMaxPixelError);
        _uiView.SetFlags(_renderFlags);
    }

    private bool _visible = true;

    void LateUpdate()
    {
        if (_uiView != null && _visible)
        {
            UpdateInputs();
            UpdateSettings();
            NoesisUnity.IME.Update(_uiView);
            NoesisUnity.TouchKeyboard.Update();
            _needsRendering = _uiView.Update(_useRealTimeClock ? Time.realtimeSinceStartup : Time.time);

            if (_textureCamera != null)
            {
                if (_continuousRendering || _needsRendering)
                {
                    _textureCamera.Render();
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        if (_uiView != null && _textureCamera != null)
        {
            _visible = false;
        }
    }

    void OnBecameVisible()
    {
        if (_uiView != null && _textureCamera != null)
        {
            _visible = true;
        }
    }

    private void OnPreRender()
    {
        if (_uiView != null && _visible)
        {
            _commands.Clear();
            _commands.name = "NoesisView_Offscreen";
            NoesisRenderer.RenderOffscreen(_uiView, _commands);
            Graphics.ExecuteCommandBuffer(_commands);

            // CommandBuffer.IssuePluginEventAndData does not invalidate state (CommandBuffer.IssuePluginEvent does)
            GL.InvalidateState();

            // Unity should restore the render target at this point but sometimes (for example a scene without lights)
            // it doesn't. We use this hack to flush the active render target and force unity to set the camera RT afterward
            RenderTexture surface = RenderTexture.GetTemporary(1,1);
            Graphics.SetRenderTarget(surface);
            RenderTexture.ReleaseTemporary(surface);
        }
    }

    private bool IsGL()
    {
        return
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore;
    }

    private bool FlipRender()
    {
#if UNITY_5_6_OR_NEWER
        // In D3D when Unity is rendering to an intermmediate texture instead of the back buffer, we need to vertically flip the output
        // Note that camera.activeTexture should only be checked from OnPostRender
        if (!IsGL())
        {
            UnityEngine.Camera camera = GetComponent<Camera>();
            return camera.activeTexture != null;
        }

        return false;
#else
        // This path doesn't catch all scenarios correctly, but there is no better way for <5.6
        if (!IsGL())
        {
            UnityEngine.Camera camera = GetComponent<Camera>();

            return camera.targetTexture != null ||
                camera.actualRenderingPath == UnityEngine.RenderingPath.DeferredLighting ||
                camera.actualRenderingPath == UnityEngine.RenderingPath.DeferredShading;
        }

        return false;
#endif
    }

    void OnPostRender()
    {
        if (_uiView != null && _visible)
        {
            _commands.Clear();
            _commands.name = "NoesisView_Onscreen";
            NoesisRenderer.RenderOnscreen(_uiView, FlipRender(), _commands);
            Graphics.ExecuteCommandBuffer(_commands);

            GL.InvalidateState();

            if (_texture != null)
            {
                _texture.DiscardContents(false, true);
            }
        }
    }

    private UnityEngine.EventModifiers _modifiers = 0;

    private void ProcessModifierKey(EventModifiers modifiers, EventModifiers delta, EventModifiers flag, Noesis.Key key)
    {
        if ((delta & flag) > 0)
        {
            if ((modifiers & flag) > 0)
            {
                _uiView.KeyDown(key);
            }
            else
            {
                _uiView.KeyUp(key);
            }
        }
    }

    private bool HitTest(float x, float y)
    {
        return VisualTreeHelper.HitTest(_uiView.Content, new Point(x, y)).VisualHit != null;
    }

#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
    private static int lastFrame;
    private static Noesis.Key lastKeyDown;
#endif

    private void ProcessEvent(UnityEngine.Event ev, bool enableKeyboard, bool enableMouse, bool emulateTouch)
    {
        // Process keyboard modifiers
        if (enableKeyboard)
        {
            EventModifiers delta = ev.modifiers ^ _modifiers;
            if (delta > 0)
            {
                _modifiers = ev.modifiers;

                ProcessModifierKey(ev.modifiers, delta, EventModifiers.Shift, Key.LeftShift);
                ProcessModifierKey(ev.modifiers, delta, EventModifiers.Control, Key.LeftCtrl);
                ProcessModifierKey(ev.modifiers, delta, EventModifiers.Command, Key.LeftCtrl);
                ProcessModifierKey(ev.modifiers, delta, EventModifiers.Alt, Key.LeftAlt);
            }
        }

        switch (ev.type)
        {
            case UnityEngine.EventType.MouseDown:
            {
                if (enableMouse)
                {
                    UnityEngine.Vector2 mouse = ProjectPointer(ev.mousePosition.x, UnityEngine.Screen.height - ev.mousePosition.y);

                    if (HitTest(mouse.x, mouse.y))
                    {
                        ev.Use();
                    }

                    if (emulateTouch)
                    {
                        _uiView.TouchDown((int)mouse.x, (int)mouse.y, 0);
                        _touchEmulated = true;
                    }
                    else
                    {
                        // Ignore events generated by Unity to simulate a mouse down when a touch event occurs
                        bool mouseEmulated = Input.simulateMouseWithTouches && Input.touchCount > 0;
                        if (!mouseEmulated)
                        {
                            if (ev.clickCount == 1)
                            {
                                _uiView.MouseButtonDown((int)mouse.x, (int)mouse.y, (Noesis.MouseButton)ev.button);
                            }
                            else
                            {
                                _uiView.MouseDoubleClick((int)mouse.x, (int)mouse.y, (Noesis.MouseButton)ev.button);
                            }
                        }
                    }
                }
                break;
            }
            case UnityEngine.EventType.MouseUp:
            {
                if (enableMouse)
                {
                    UnityEngine.Vector2 mouse = ProjectPointer(ev.mousePosition.x, UnityEngine.Screen.height - ev.mousePosition.y);

                    if (HitTest(mouse.x, mouse.y))
                    {
                        ev.Use();
                    }

                    if (emulateTouch && _touchEmulated)
                    {
                        _uiView.TouchUp((int)mouse.x, (int)mouse.y, 0);
                        _touchEmulated = false;
                    }
                    else
                    {
                        // Ignore events generated by Unity to simulate a mouse up when a touch event occurs
                        bool mouseEmulated = Input.simulateMouseWithTouches && Input.touchCount > 0;
                        if (!mouseEmulated)
                        {
                            _uiView.MouseButtonUp((int)mouse.x, (int)mouse.y, (Noesis.MouseButton)ev.button);
                        }
                    }
                }
                break;
            }
            case UnityEngine.EventType.KeyDown:
            {
                if (enableKeyboard)
                {
                    // Don't process key when IME composition is being used
                    if (ev.keyCode != KeyCode.None && NoesisUnity.IME.compositionString == "")
                    {
                        Noesis.Key noesisKeyCode = NoesisKeyCodes.Convert(ev.keyCode);
                        if (noesisKeyCode != Noesis.Key.None)
                        {
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
                            // In OSX Standalone, CMD + key always sends two KeyDown events for the key.
                            // This seems to be a bug in Unity. 
                            if (!ev.command || lastFrame != Time.frameCount || lastKeyDown != noesisKeyCode)
                            {
                                lastFrame = Time.frameCount;
                                lastKeyDown = noesisKeyCode;
#endif
                                _uiView.KeyDown(noesisKeyCode);
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
                            }
#endif
                        }
                    }

                    if (ev.character != 0)
                    {
                        // Filter out character events when CTRL is down
                        bool isControl = (_modifiers & EventModifiers.Control) != 0 || (_modifiers & EventModifiers.Command) != 0;
                        bool isAlt = (_modifiers & EventModifiers.Alt) != 0;
                        bool filter = isControl && !isAlt;

                        if (!filter)
                        {
#if !UNITY_EDITOR && UNITY_STANDALONE_LINUX
                            // It seems that linux is sending KeySyms instead of Unicode points
                            // https://github.com/substack/node-keysym/blob/master/data/keysyms.txt
                            ev.character = NoesisKeyCodes.KeySymToUnicode(ev.character);
#endif
                            _uiView.Char((uint)ev.character);
                        }
                    }

                }
                break;
            }
            case UnityEngine.EventType.KeyUp:
            {
                // Don't process key when IME composition is being used
                if (enableKeyboard)
                {
                    if (ev.keyCode != KeyCode.None && NoesisUnity.IME.compositionString == "")
                    {
                        Noesis.Key noesisKeyCode = NoesisKeyCodes.Convert(ev.keyCode);
                        if (noesisKeyCode != Noesis.Key.None)
                        {
                            _uiView.KeyUp(noesisKeyCode);
                        }
                    }
                }
                break;
            }
        }
    }

    void OnGUI()
    {
        if (_uiView != null)
        {
            ProcessEvent(UnityEngine.Event.current, _enableKeyboard, _enableMouse, _emulateTouch);
        }
    }

    void OnApplicationFocus(bool focused)
    {
        if (_uiView != null)
        {
            if (NoesisUnity.TouchKeyboard.keyboard == null)
            {
                if (focused)
                {
                    _uiView.Activate();
                }
                else
                {
                    _uiView.Deactivate();
                }
            }
        }
    }
#endregion

    void SetRenderSettings()
    {
        NoesisSettings settings = NoesisSettings.Get();

        bool linearRendering = false;

        switch (settings.linearRendering)
        {
            case NoesisSettings.LinearRendering._SamesAsUnity:
            {
                linearRendering = QualitySettings.activeColorSpace == ColorSpace.Linear;
                break;
            }
            case NoesisSettings.LinearRendering._Enabled:
            {
                linearRendering = true;
                break;
            }
            case NoesisSettings.LinearRendering._Disabled:
            {
                linearRendering = false;
                break;
            }
        }

        int sampleCount = 1;

        switch (settings.offscreenSampleCount)
        {
            case NoesisSettings.OffscreenSampleCount._SameAsUnity:
            {
                sampleCount = QualitySettings.antiAliasing;
                break;
            }
            case NoesisSettings.OffscreenSampleCount._1x:
            {
                sampleCount = 1;
                break;
            }
            case NoesisSettings.OffscreenSampleCount._2x:
            {
                sampleCount = 2;
                break;
            }
            case NoesisSettings.OffscreenSampleCount._4x:
            {
                sampleCount = 4;
                break;
            }
            case NoesisSettings.OffscreenSampleCount._8x:
            {
                sampleCount = 8;
                break;
            }
        }

        uint offscreenDefaultNumSurfaces = settings.offscreenInitSurfaces;
        uint offscreenMaxNumSurfaces = settings.offscreenMaxSurfaces;
        uint glyphCacheMeshThreshold = settings.glyphMeshThreshold;

        int glyphCacheTextureWidth = 1024;
        int glyphCacheTextureHeight = 1024;

        switch (settings.glyphTextureSize)
        {
            case NoesisSettings.TextureSize._256x256:
            {
                glyphCacheTextureWidth = 256;
                glyphCacheTextureHeight = 256;
                break;
            }
            case NoesisSettings.TextureSize._512x512:
            {
                glyphCacheTextureWidth = 512;
                glyphCacheTextureHeight = 512;
                break;
            }
            case NoesisSettings.TextureSize._1024x1024:
            {
                glyphCacheTextureWidth = 1024;
                glyphCacheTextureHeight = 1024;
                break;
            }
            case NoesisSettings.TextureSize._2048x2048:
            {
                glyphCacheTextureWidth = 2048;
                glyphCacheTextureHeight = 2048;
                break;
            }
            case NoesisSettings.TextureSize._4096x4096:
            {
                glyphCacheTextureWidth = 4096;
                glyphCacheTextureHeight = 4096;
                break;
            }
        }

        int colorGlyphCacheTextureWidth = 0;
        int colorGlyphCacheTextureHeight = 0;

        switch (settings.colorGlyphTextureSize)
        {
            case NoesisSettings.ColorTextureSize._Auto:
            {
                colorGlyphCacheTextureWidth = 0;
                colorGlyphCacheTextureHeight = 0;
                break;
            }
            case NoesisSettings.ColorTextureSize._256x256:
            {
                colorGlyphCacheTextureWidth = 256;
                colorGlyphCacheTextureHeight = 256;
                break;
            }
            case NoesisSettings.ColorTextureSize._512x512:
            {
                colorGlyphCacheTextureWidth = 512;
                colorGlyphCacheTextureHeight = 512;
                break;
            }
            case NoesisSettings.ColorTextureSize._1024x1024:
            {
                colorGlyphCacheTextureWidth = 1024;
                colorGlyphCacheTextureHeight = 1024;
                break;
            }
            case NoesisSettings.ColorTextureSize._2048x2048:
            {
                colorGlyphCacheTextureWidth = 2048;
                colorGlyphCacheTextureHeight = 2048;
                break;
            }
            case NoesisSettings.ColorTextureSize._4096x4096:
            {
                colorGlyphCacheTextureWidth = 4096;
                colorGlyphCacheTextureHeight = 4096;
                break;
            }
        }

        Noesis_RendererSettings(linearRendering, sampleCount, offscreenDefaultNumSurfaces,
            offscreenMaxNumSurfaces, glyphCacheTextureWidth, glyphCacheTextureHeight,
            colorGlyphCacheTextureWidth, colorGlyphCacheTextureHeight, glyphCacheMeshThreshold);
    }

    private void CreateView(FrameworkElement content)
    {
        if (_uiView == null)
        {
            // Send settings for the internal device, created by the first view
            SetRenderSettings();

            _uiView = new Noesis.View(content);

            _commands.Clear();
            NoesisRenderer.RegisterView(_uiView, _commands);
            Graphics.ExecuteCommandBuffer(_commands);
        }
    }

    private void DestroyView()
    {
        if (_uiView != null)
        {
            _commands.Clear();
            NoesisRenderer.UnregisterView(_uiView, _commands);
            Graphics.ExecuteCommandBuffer(_commands);

            _uiView = null;
        }
    }

    private Noesis.View _uiView;
    private Camera _textureCamera;
    private UnityEngine.Rendering.CommandBuffer _commands;
    private bool _needsRendering = false;

#region Serialized properties
    public NoesisXaml _xaml;
    public RenderTexture _texture;

    public bool _isPPAAEnabled = true;
    public float _tessellationMaxPixelError = Noesis.TessellationMaxPixelError.MediumQuality.Error;
    public RenderFlags _renderFlags = 0;
    public bool _continuousRendering = true;

    public bool _enableKeyboard = true;
    public bool _enableMouse = true;
    public bool _enableTouch = true;
    public bool _emulateTouch = false;
    public bool _useRealTimeClock = false;
#endregion

#region Imports
    [DllImport(Library.Name)]
    static extern void Noesis_RendererSettings(bool linearSpaceRendering, int offscreenSampleCount,
        uint offscreenDefaultNumSurfaces, uint offscreenMaxNumSurfaces, int glyphCacheTextureWidth,
        int glyphCacheTextureHeight, int colorGlyphCacheTextureWidth, int colorGlyphCacheTextureHeight,
        uint glyphCacheMeshTreshold);
#endregion

#endregion
}
