using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace UnityProject.Editor
{

    public sealed class TextureAlphaPremultiplier : AssetPostprocessor
    {

        public void OnPreprocessTexture()
        {
            if (ShouldPremultiplyAlpha())
                ((TextureImporter)assetImporter).alphaIsTransparency = false;
        }

        public void OnPostprocessTexture([NotNull] Texture2D texture)
        {
            if (!ShouldPremultiplyAlpha())
                return;

            int width = texture.width;
            int height = texture.height;
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    Color color = texture.GetPixel(x, y);
                    texture.SetPixel(x, y, Premultiply(color));
                }
            }

            texture.Apply();

            Debug.Log("Automatically premultiplied alpha for " + assetPath);
        }

        private static Color Premultiply(Color color)
        {
            float a = color.a;
            return new Color(color.r * a, color.g * a, color.b * a, a);
        }

        [Pure]
        private bool ShouldPremultiplyAlpha()
        {
            return assetPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

    }

}