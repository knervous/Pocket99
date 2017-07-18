using System;

namespace Noesis
{
    public partial class TextureSource
    {
        public TextureSource(UnityEngine.Texture texture): this(Texture.WrapTexture(texture,
            EnsureNativePointer(texture), texture.width, texture.height, 1))
        {
        }

        public TextureSource(UnityEngine.Texture2D texture): this(Texture.WrapTexture(texture,
            EnsureNativePointer(texture), texture.width, texture.height, texture.mipmapCount))
        {
        }

        private static IntPtr EnsureNativePointer(UnityEngine.Texture tex)
        {
            IntPtr nativeTexturePtr = tex.GetNativeTexturePtr();
            if (nativeTexturePtr == IntPtr.Zero)
            {
                UnityEngine.RenderTexture renderTexture = tex as UnityEngine.RenderTexture;
                if (renderTexture != null)
                {
                    renderTexture.Create();
                }

                nativeTexturePtr = tex.GetNativeTexturePtr();
                if (nativeTexturePtr == IntPtr.Zero)
                {
                    throw new System.Exception("Can't create TextureSource, texture native pointer is null");
                }
            }

            return nativeTexturePtr;
        }
    }
}