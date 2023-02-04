using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Tools
{
    public static class Tex2DExtension
    {
        public static UniTask DrawCircle(this Texture2D texture, Color color, int x, int y, int radius = 3)
        {
            float rSquared = radius * radius;
            var pixels = texture.GetPixels32();

            var textureWidth = texture.width;
            var textureHeight = texture.height;
            var colorA = color.a;

            for (var u = x - radius; u < x + radius + 1; u++)
            for (var v = y - radius; v < y + radius + 1; v++)
                if (u > 0 && v > 0 && u < textureWidth && v < textureHeight && (x - u) * (x - u) + (y - v) * (y - v) < rSquared && pixels[v * textureWidth + u].a > colorA)
                    pixels[v * textureWidth + u] = color;

            texture.SetPixels32(pixels);
            return UniTask.CompletedTask;
        }
    }
}