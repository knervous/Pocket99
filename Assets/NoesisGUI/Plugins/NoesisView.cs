using UnityEngine;
using Noesis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
[AddComponentMenu("NoesisGUI/Noesis View")]
public class NoesisView: MonoBehaviour
{
    #region Public properties

    /// <summary>
    /// Path to the XAML that will be loaded when this component is enabled.
    /// </summary>
    public NoesisXaml Xaml
    {
        set { this._xaml = value; }
        get { return this._xaml; }
    }

    /// <summary>
    /// Selects antialiasing mode.
    /// </summary>
    public View.AntialiasingMode AntiAliasingMode
    {
        set { this._antiAliasingMode = value; }
        get { return this._antiAliasingMode; }
    }

    /// <summary>
    /// Determines the quantity of triangles generated for vector shapes.
    /// </summary>
    public View.TessellationQuality TessellationQuality
    {
        set { this._tessellationQuality = value; }
        get { return this._tessellationQuality; }
    }

    /// <summary>
    /// Bit flags used for debug rendering purposes.
    /// </summary>
    public View.RenderFlags RenderFlags
    {
        set { this._renderFlags = value; }
        get { return this._renderFlags; }
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
        return FindRenderTexture() != null;
    }

    #endregion

    #region Public events

    #region Keyboard input events
    /// <summary>
    /// Notifies Renderer that a key was pressed.
    /// </summary>
    /// <param name="key">Key identifier.</param>
    public void KeyDown(Noesis.Key key)
    {
        if (_uiView != null)
        {
            _uiView.KeyDown(key);
        }
    }

    /// <summary>
    /// Notifies Renderer that a key was released.
    /// </summary>
    /// <param name="key">Key identifier.</param>
    public void KeyUp(Noesis.Key key)
    {
        if (_uiView != null)
        {
            _uiView.KeyUp(key);
        }
    }

    /// <summary>
    /// Notifies Renderer that a key was translated to the corresponding character.
    /// </summary>
    /// <param name="ch">Unicode character value.</param>
    public void Char(uint ch)
    {
        if (_uiView != null)
        {
            _uiView.Char(ch);
        }
    }
    #endregion

    #region Mouse input events
    /// <summary>
    /// Notifies Renderer that mouse was moved. The mouse position is specified in renderer
    /// surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    public void MouseMove(int x, int y)
    {
        if (_uiView != null)
        {
            _uiView.MouseMove(x, y);
        }
    }

    /// <summary>
    /// Notifies Renderer that a mouse button was pressed. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="button">Indicates which button was pressed.</param>
    public void MouseDown(int x, int y, Noesis.MouseButton button)
    {
        if (_uiView != null)
        {
            _uiView.MouseDown(x, y, button);
        }
    }

    /// Notifies Renderer that a mouse button was released. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="button">Indicates which button was released.</param>
    public void MouseUp(int x, int y, Noesis.MouseButton button)
    {
        if (_uiView != null)
        {
            _uiView.MouseUp(x, y, button);
        }
    }

    /// <summary>
    /// Notifies Renderer of a mouse button double click. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="button">Indicates which button was pressed.</param>
    public void MouseDoubleClick(int x, int y, Noesis.MouseButton button)
    {
        if (_uiView != null)
        {
            _uiView.MouseDoubleClick(x, y, button);
        }
    }

    /// <summary>
    /// Notifies Renderer that mouse wheel was rotated. The mouse position is specified in
    /// renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Mouse x-coordinate.</param>
    /// <param name="y">Mouse y-coordinate.</param>
    /// <param name="wheelRotation">Indicates the amount mouse wheel has changed.</param>
    public void MouseWheel(int x, int y, int wheelRotation)
    {
        if (_uiView != null)
        {
            _uiView.MouseWheel(x, y, wheelRotation);
        }
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
    public void TouchMove(int x, int y, uint touchId)
    {
        if (_uiView != null)
        {
            _uiView.TouchMove(x, y, touchId);
        }
    }

    /// <summary>
    /// Notifies Renderer that a finger touches the screen. The finger position is
    /// specified in renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Finger x-coordinate.</param>
    /// <param name="y">Finger y-coordinate.</param>
    /// <param name="touchId">Finger identifier.</param>
    public void TouchDown(int x, int y, uint touchId)
    {
        if (_uiView != null)
        {
            _uiView.TouchDown(x, y, touchId);
        }
    }

    /// <summary>
    /// Notifies Renderer that a finger is raised off the screen. The finger position is
    /// specified in renderer surface pixel coordinates.
    /// </summary>
    /// <param name="x">Finger x-coordinate.</param>
    /// <param name="y">Finger y-coordinate.</param>
    /// <param name="touchId">Finger identifier.</param>
    public void TouchUp(int x, int y, uint touchId)
    {
        if (_uiView != null)
        {
            _uiView.TouchUp(x, y, touchId);
        }
    }
    #endregion

    /// <summary>
    /// Occurs just before the objects in the UI tree are rendered.
    /// </summary>
    /*public event System.EventHandler Rendering
    {
        add { if (_uiView != null) { _uiView.Rendering += value; } }
        remove { if (_uiView != null) { _uiView.Rendering -= value; } }
    }*/

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
        _antiAliasingMode = View.AntialiasingMode.MSAA;
        _tessellationQuality = View.TessellationQuality.Medium;
        _renderFlags = 0;
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
        _texture = FindRenderTexture();

        if (_texture != null && _textureCamera == null)
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
        }

        LoadXaml(false);

        Camera.onPreRender += PreRender;
    }

    void OnDisable()
    {
        Camera.onPreRender -= PreRender;
    }

    void OnDestroy()
    {
        if (_textureCamera != null)
        {
            UnityEngine.Object.Destroy(_textureCamera);
            _textureCamera = null;
        }

        _texture = null;

        DestroyView();
    }

    UnityEngine.EventSystems.PointerEventData _pointerData;

    private UnityEngine.Vector2 ProjectPointer(float x, float y)
    {
        if (_texture == null)
        {
            return new UnityEngine.Vector2(x, UnityEngine.Screen.height - y);
        }
        else
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
            // texture coordintates, otherwise Hit Testing won't work

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
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        Select = 16,
        Back = 32
    }

    private VirtualKeys _virtualKeys = 0;

    private void UpdateVirtualKeys()
    {
        VirtualKeys virtualKeys = 0;

        try
        {
            if (Input.GetAxis("Noesis_Vertical") > 0.5f)
            {
                virtualKeys |= VirtualKeys.Up;
            }
        }
        catch (Exception) {}

        try
        {
            if (Input.GetAxis("Noesis_Vertical") < -0.5f)
            {
                virtualKeys |= VirtualKeys.Down;
            }
        }
        catch (Exception) {}

        try
        {
            if (Input.GetAxis("Noesis_Horizontal") > 0.5f)
            {
                virtualKeys |= VirtualKeys.Right;
            }
        }
        catch (Exception) {}

        try
        {
            if (Input.GetAxis("Noesis_Horizontal") < -0.5f)
            {
                virtualKeys |= VirtualKeys.Left;
            }
        }
        catch (Exception) {}

        try
        {
            if (Input.GetButton("Noesis_Select"))
            {
                virtualKeys |= VirtualKeys.Select;
            }
        }
        catch (Exception) {}

        try
        {
            if (Input.GetButton("Noesis_Back"))
            {
                virtualKeys |= VirtualKeys.Back;
            }
        }
        catch (Exception) {}

        VirtualKeys delta = virtualKeys ^ _virtualKeys;
        if (delta != 0)
        {
            if ((delta & VirtualKeys.Up) > 0)
            {
                if ((virtualKeys & VirtualKeys.Up) > 0)
                {
                    _uiView.KeyDown(Key.Up);
                }
                else
                {
                    _uiView.KeyUp(Key.Up);
                }
            }

            if ((delta & VirtualKeys.Down) > 0)
            {
                if ((virtualKeys & VirtualKeys.Down) > 0)
                {
                    _uiView.KeyDown(Key.Down);
                }
                else
                {
                    _uiView.KeyUp(Key.Down);
                }
            }

            if ((delta & VirtualKeys.Right) > 0)
            {
                if ((virtualKeys & VirtualKeys.Right) > 0)
                {
                    _uiView.KeyDown(Key.Right);
                }
                else
                {
                    _uiView.KeyUp(Key.Right);
                }
            }

            if ((delta & VirtualKeys.Left) > 0)
            {
                if ((virtualKeys & VirtualKeys.Left) > 0)
                {
                    _uiView.KeyDown(Key.Left);
                }
                else
                {
                    _uiView.KeyUp(Key.Left);
                }
            }

            if ((delta & VirtualKeys.Select) > 0)
            {
                if ((virtualKeys & VirtualKeys.Select) > 0)
                {
                    _uiView.KeyDown(Key.Space);
                }
                else
                {
                    _uiView.KeyUp(Key.Space);
                }
            }

            if ((delta & VirtualKeys.Back) > 0)
            {
                if ((virtualKeys & VirtualKeys.Back) > 0)
                {
                    _uiView.KeyDown(Key.Escape);
                }
                else
                {
                    _uiView.KeyUp(Key.Space);
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
        _uiView.SetSize(camera.pixelWidth, camera.pixelHeight);
        _uiView.SetAntialiasingMode(_antiAliasingMode);
        _uiView.SetTessellationQuality(_tessellationQuality);
        _uiView.SetFlags(_renderFlags);
    }

    void LateUpdate()
    {
        if (_uiView != null)
        {
            UpdateInputs();
            UpdateSettings();
           _uiView.Update(_useRealTimeClock ? Time.realtimeSinceStartup : Time.time);
        }
    }

    private bool _visible = true;

    void OnBecameInvisible()
    {
        if (_uiView != null && _texture != null)
        {
            _visible = false;
        }
    }

    void OnBecameVisible()
    {
        if (_uiView != null && _texture != null)
        {
            _visible = true;
        }
    }

    private bool _updatePending = true;

    private void PreRender(Camera cam)
    {
        // We need the offscreen phase to happen before any camera rendering. This is critical for tiled architectures.
        // This method is invoked for each camera in the scene. We enqueue the offscreen phase in the first camera and disable for rest of cameras
        if (_updatePending)
        {
            RenderOffscreen();
            _updatePending = false;
        }
    }

    void RenderOffscreen()
    {
        if (_uiView != null && _visible)
        {
            NoesisRenderer.RenderOffscreen(_uiView);

            // Unity should restore the render target at this point but sometimes (for example a scene without lights)
            // it doesn't. We use this hack to flush the active render target and force unity to set the camera RT afterward
            RenderTexture surface = RenderTexture.GetTemporary(1,1);
            Graphics.SetRenderTarget(surface);
            RenderTexture.ReleaseTemporary(surface);
        }
    }

    private bool IsD3D()
    {
        return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D9 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D12 ||
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.XboxOne;
    }

    private bool FlipRender()
    {
#if UNITY_5_6_OR_NEWER
        // In D3D when Unity is rendering to an intermmediate texture instead of the back buffer, we need to vertically flip the output
        // Note that camera.activeTexture should only be checked from OnPostRender
        if (IsD3D())
        {
            UnityEngine.Camera camera = GetComponent<Camera>();
            return camera.activeTexture != null;
        }

        return false;
#else
        // This path doesn't catch all scenarios correctly, but there is no better way for <5.6
        if (IsD3D())
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
            NoesisRenderer.RenderOnscreen(_uiView, FlipRender());

            if (_texture != null)
            {
                _texture.DiscardContents(false, true);
            }

            _updatePending = true;
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
                        // Ignore events generated by Unity to simulate a mouse down when a
                        // touch event occurs
                        bool mouseEmulated = Input.simulateMouseWithTouches && Input.touchCount > 0;
                        if (!mouseEmulated)
                        {
                            _uiView.MouseDown((int)mouse.x, (int)mouse.y, (Noesis.MouseButton)ev.button);

                            if (ev.clickCount == 2)
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
                        // Ignore events generated by Unity to simulate a mouse up when a
                        // touch event occurs
                        bool mouseEmulated = Input.simulateMouseWithTouches && Input.touchCount > 0;
                        if (!mouseEmulated)
                        {
                            _uiView.MouseUp((int)mouse.x, (int)mouse.y, (Noesis.MouseButton)ev.button);
                        }
                    }
                }
                break;
            }
            case UnityEngine.EventType.KeyDown:
            {
                if (enableKeyboard)
                {
                    if (ev.keyCode != KeyCode.None)
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
                if (enableKeyboard)
                {
                    if (ev.keyCode != UnityEngine.KeyCode.None)
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
        /*if (_uiView != null)
        {
            if (!NoesisGUISystem.SoftwareKeyboardManager.IsOpen)
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
        }*/
    }
    #endregion

    void SetRenderSettings()
    {
        NoesisSettings settings = NoesisSettings.Get();

        bool linearRendering = QualitySettings.activeColorSpace == ColorSpace.Linear;

        int offscreenWidth = (int)settings.offscreenTextureSize.x;
        int offscreenHeight = (int)settings.offscreenTextureSize.y;
        int sampleCount = QualitySettings.antiAliasing;

        uint offscreenDefaultNumSurfaces = settings.offscreenInitSurfaces;
        uint offscreenMaxNumSurfaces = settings.offscreenMaxSurfaces;
        uint glyphCacheMeshTreshold = settings.glyphMeshTreshold;

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

        Noesis_RendererSettings(linearRendering, offscreenWidth, offscreenHeight, sampleCount, offscreenDefaultNumSurfaces,
            offscreenMaxNumSurfaces, glyphCacheTextureWidth, glyphCacheTextureHeight, glyphCacheMeshTreshold);
    }

    private void CreateView(FrameworkElement content)
    {
        if (_uiView == null)
        {
            // Send settings for the internal device, created by the first view
            SetRenderSettings();

            _uiView = new Noesis.View(content);
            NoesisRenderer.RegisterView(_uiView);
        }
    }

    private void DestroyView()
    {
        if (_uiView != null)
        {
            NoesisRenderer.Shutdown(_uiView);
            _uiView = null;
        }
    }

    public UnityEngine.RenderTexture FindRenderTexture()
    {
        // Check if NoesisGUI was attached to a Unity GUI RawImage object with a RenderTexture
        UnityEngine.UI.RawImage img = gameObject.GetComponent<UnityEngine.UI.RawImage>();
        if (img != null && img.texture != null)
        {
            return img.texture as UnityEngine.RenderTexture;
        }

        // Check if NoesisGUI was attached to a GameObject with a RenderTexture set
        // in the diffuse texture of its main Material
        UnityEngine.Renderer renderer = gameObject.GetComponent<UnityEngine.Renderer>();
        if (renderer != null && renderer.sharedMaterial != null)
        {
            return renderer.sharedMaterial.mainTexture as UnityEngine.RenderTexture;
        }

        // No valid texture found
        return null;
    }

    private Noesis.View _uiView;

    private UnityEngine.RenderTexture _texture;
    private UnityEngine.Camera _textureCamera;

    #region Properties needed by component editor
    public NoesisXaml _xaml;

    public View.AntialiasingMode _antiAliasingMode = View.AntialiasingMode.MSAA;
    public View.TessellationQuality _tessellationQuality = View.TessellationQuality.Medium;
    public View.RenderFlags _renderFlags = 0;

    public bool _enableKeyboard = true;
    public bool _enableMouse = true;
    public bool _enableTouch = true;
    public bool _emulateTouch = false;
    public bool _useRealTimeClock = false;
    #endregion

    #region Imports
    #if UNITY_IPHONE || UNITY_XBOX360
    [DllImport("__Internal")]
    #else
    [DllImport("Noesis")]
    #endif
    static extern void Noesis_RendererSettings(bool linearSpaceRendering, int offscreenWidth, int offscreenHeight,
        int offscreenSampleCount, uint offscreenDefaultNumSurfaces, uint offscreenMaxNumSurfaces,
        int glyphCacheTextureWidth, int glyphCacheTextureHeight, uint glyphCacheMeshTreshold);
    #endregion

    #endregion
}
