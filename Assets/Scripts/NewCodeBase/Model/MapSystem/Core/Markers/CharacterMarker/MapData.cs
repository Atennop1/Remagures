using System;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public readonly struct MapData
    {
        public readonly Texture2D MapTexture;
        public readonly Vector2Int WorldSize;
        public readonly Vector2Int WorldOffset;

        public MapData(Texture2D mapTexture, Vector2Int worldSize, Vector2Int worldOffset)
        {
            MapTexture = mapTexture ?? throw new ArgumentNullException(nameof(mapTexture));
            WorldSize = worldSize;
            WorldOffset = worldOffset;
        }
    }
}