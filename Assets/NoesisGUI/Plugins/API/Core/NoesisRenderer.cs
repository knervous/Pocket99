using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class Renderer
    {
        /// <summary>
        /// Initializes the Renderer with a GL render device and the provided options.
        /// </summary>
        /// <param name="vgOptions">Vector graphics options.</param>
        public void InitGL(VGOptions vgOptions)
        {
            Noesis_Renderer_InitGL_(CPtr,
                vgOptions.OffscreenWidth, vgOptions.OffscreenHeight, vgOptions.OffscreenSampleCount,
                vgOptions.OffscreenDefaultNumSurfaces, vgOptions.OffscreenMaxNumSurfaces,
                vgOptions.GlyphCacheTextureWidth, vgOptions.GlyphCacheTextureHeight,
                vgOptions.GlyphCacheMeshTreshold);
        }

        /// <summary>
        /// Initializes the Renderer with a D3D11 render device and the provided options.
        /// </summary>
        /// <param name="vgOptions">Vector graphics options.</param>
        public void InitD3D11(IntPtr deviceContext, VGOptions vgOptions)
        {
            Noesis_Renderer_InitD3D11_(CPtr, deviceContext, false,
                vgOptions.OffscreenWidth, vgOptions.OffscreenHeight, vgOptions.OffscreenSampleCount,
                vgOptions.OffscreenDefaultNumSurfaces, vgOptions.OffscreenMaxNumSurfaces,
                vgOptions.GlyphCacheTextureWidth, vgOptions.GlyphCacheTextureHeight,
                vgOptions.GlyphCacheMeshTreshold);
        }

        /// <summary>
        /// Initializes the Renderer with a D3D11 render device and the provided options.
        /// </summary>
        /// <param name="sRGB">Enables linear rendering.</param>
        /// <param name="vgOptions">Vector graphics options.</param>
        public void InitD3D11(IntPtr deviceContext, bool sRGB, VGOptions vgOptions)
        {
            Noesis_Renderer_InitD3D11_(CPtr, deviceContext, sRGB,
                vgOptions.OffscreenWidth, vgOptions.OffscreenHeight, vgOptions.OffscreenSampleCount,
                vgOptions.OffscreenDefaultNumSurfaces, vgOptions.OffscreenMaxNumSurfaces,
                vgOptions.GlyphCacheTextureWidth, vgOptions.GlyphCacheTextureHeight,
                vgOptions.GlyphCacheMeshTreshold);
        }

        /// <summary>
        /// Free allocated render resources and render tree
        /// </summary>
        public void Shutdown()
        {
            Noesis_Renderer_Shutdown_(CPtr);
        }

        /// <summary>
        /// Determines the visible region. By default it is set to cover the view dimensions.
        /// </summary>
        /// <param name="x">Horizontal start of visible region.</param>
        /// <param name="y">Vertical start of visible region.</param>
        /// <param name="width">Horizontal size of visible region.</param>
        /// <param name="height">Vertical size of visible region.</param>
        public void SetRenderRegion(float x, float y, float width, float height)
        {
            Noesis_Renderer_SetRenderRegion_(CPtr, x, y, width, height);
        }

        /// <summary>
        /// Applies last changes happened in the view. This function does not interacts with the
        /// render device. Returns whether the render tree really changed.
        /// </summary>
        public bool UpdateRenderTree()
        {
            return Noesis_Renderer_UpdateRenderTree_(CPtr);
        }

        /// <summary>
        /// Indicates if offscreen textures are needed at the current render tree state. When this
        /// function returns true, it is mandatory to call RenderOffscreen() before Render()
        /// </summary>
        public bool NeedsOffscreen()
        {
            return Noesis_Renderer_NeedsOffscreen_(CPtr);
        }

        /// <summary>
        /// Generates offscreen textures. This function fills internal textures and must be invoked
        /// before binding the main render target. This is especially critical in tiled
        /// architectures.
        /// </summary>
        public void RenderOffscreen()
        {
            Noesis_Renderer_RenderOffscreen_(CPtr);
        }

        /// <summary>
        /// Renders UI in the active render target and viewport dimensions
        /// </summary>
        public void Render()
        {
            Noesis_Renderer_Render_(CPtr);
        }

        #region Private members
        internal Renderer(View view)
        {
            _view = view;
        }

        private HandleRef CPtr { get { return _view.CPtr; } }

        View _view;
        #endregion

        #region Imports
        static void Noesis_Renderer_InitGL_(HandleRef renderer,
            uint offscreenWidth, uint offscreenHeight, uint offscreenSampleCount,
            uint offscreenDefaultNumSurfaces, uint offscreenMaxNumSurfaces,
            uint glyphCacheTextureWidth, uint glyphCacheTextureHeight, uint glyphCacheMeshTreshold)
        {
            Noesis_Renderer_InitGL(renderer,
                offscreenWidth, offscreenHeight, offscreenSampleCount,
                offscreenDefaultNumSurfaces, offscreenMaxNumSurfaces,
                glyphCacheTextureWidth, glyphCacheTextureHeight, glyphCacheMeshTreshold);
            Error.Check();
        }

        static void Noesis_Renderer_InitD3D11_(HandleRef renderer, IntPtr deviceContext, bool sRGB,
            uint offscreenWidth, uint offscreenHeight, uint offscreenSampleCount,
            uint offscreenDefaultNumSurfaces, uint offscreenMaxNumSurfaces,
            uint glyphCacheTextureWidth, uint glyphCacheTextureHeight, uint glyphCacheMeshTreshold)
        {
            Noesis_Renderer_InitD3D11(renderer, deviceContext, sRGB,
                offscreenWidth, offscreenHeight, offscreenSampleCount,
                offscreenDefaultNumSurfaces, offscreenMaxNumSurfaces,
                glyphCacheTextureWidth, glyphCacheTextureHeight, glyphCacheMeshTreshold);
            Error.Check();
        }

        static void Noesis_Renderer_Shutdown_(HandleRef renderer)
        {
            Noesis_Renderer_Shutdown(renderer);
            Error.Check();
        }

        static void Noesis_Renderer_SetRenderRegion_(HandleRef renderer,
            float x, float y, float width, float height)
        {
            Noesis_Renderer_SetRenderRegion(renderer, x, y, width, height);
            Error.Check();
        }

        static bool Noesis_Renderer_UpdateRenderTree_(HandleRef renderer)
        {
            bool ret = Noesis_Renderer_UpdateRenderTree(renderer);
            Error.Check();
            return ret;
        }

        static bool Noesis_Renderer_NeedsOffscreen_(HandleRef renderer)
        {
            bool ret = Noesis_Renderer_NeedsOffscreen(renderer);
            Error.Check();
            return ret;
        }

        static void Noesis_Renderer_RenderOffscreen_(HandleRef renderer)
        {
            Noesis_Renderer_RenderOffscreen(renderer);
            Error.Check();
        }

        static void Noesis_Renderer_Render_(HandleRef renderer)
        {
            Noesis_Renderer_Render(renderer);
            Error.Check();
        }

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_InitGL(HandleRef renderer,
            uint offscreenWidth, uint offscreenHeight, uint offscreenSampleCount,
            uint offscreenDefaultNumSurfaces, uint offscreenMaxNumSurfaces,
            uint glyphCacheTextureWidth, uint glyphCacheTextureHeight, uint glyphCacheMeshTreshold);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_InitD3D11(HandleRef renderer, IntPtr deviceContext,
            bool sRGB, uint offscreenWidth, uint offscreenHeight, uint offscreenSampleCount,
            uint offscreenDefaultNumSurfaces, uint offscreenMaxNumSurfaces,
            uint glyphCacheTextureWidth, uint glyphCacheTextureHeight, uint glyphCacheMeshTreshold);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_Shutdown(HandleRef renderer);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_SetRenderRegion(HandleRef renderer,
            float x, float y, float width, float height);

        [DllImport(Library.Name)]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Noesis_Renderer_UpdateRenderTree(HandleRef renderer);

        [DllImport(Library.Name)]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Noesis_Renderer_NeedsOffscreen(HandleRef renderer);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_RenderOffscreen(HandleRef renderer);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_Render(HandleRef renderer);
        #endregion
    }

    /// <summary>
    /// Vector graphics Renderer initialization options
    /// </summary>
    public class VGOptions
    {
        /// <summary>
        /// Width dimension of offscreen textures (0 = automatic). Default = 0.
        /// </summary>
        public uint OffscreenWidth { get; set; }


        /// <summary>
        /// Height dimension of offscreen textures (0 = automatic). Default = 0.
        /// </summary>
        public uint OffscreenHeight { get; set; }

        /// <summary>
        /// Multisampling of offscreen textures. Default = 1.
        /// </summary>
        public uint OffscreenSampleCount { get; set; }

        /// <summary>
        /// Number of offscreen textures created at startup. Default = 0.
        /// </summary>
        public uint OffscreenDefaultNumSurfaces { get; set; }

        /// <summary>
        /// Maximum number of offscreen textures (0 = unlimited). Default = 0.
        /// </summary>
        public uint OffscreenMaxNumSurfaces { get; set; }

        /// <summary>
        /// Width dimension of texture used to cache glyphs. Default = 1024.
        /// </summary>
        public uint GlyphCacheTextureWidth { get; set; }

        /// <summary>
        /// Height dimension of texture used to cache glyphs. Default = 1024.
        /// </summary>
        public uint GlyphCacheTextureHeight { get; set; }

        /// <summary>
        /// Glyphs with size above this are rendered using triangles meshes. Default = 48.
        /// </summary>
        public uint GlyphCacheMeshTreshold { get; set; }

        public VGOptions()
        {
            OffscreenWidth = 0;
            OffscreenHeight = 0;
            OffscreenSampleCount = 1;
            OffscreenDefaultNumSurfaces = 0;
            OffscreenMaxNumSurfaces = 0;
            GlyphCacheTextureWidth = 1024;
            GlyphCacheTextureHeight = 1024;
            GlyphCacheMeshTreshold = 96;
        }
    }
}
