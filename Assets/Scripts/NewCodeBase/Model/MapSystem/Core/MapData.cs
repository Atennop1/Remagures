using System;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public readonly struct MapData : IMapData
    {
        public Texture2D MapTexture { get; }
        public Vector2Int WorldSize { get; }
        public Vector2Int WorldOffset { get; }

        public MapData(Texture2D mapTexture, Vector2Int worldSize, Vector2Int worldOffset)
        {
            MapTexture = mapTexture ?? throw new ArgumentNullException(nameof(mapTexture));
            WorldSize = worldSize;
            WorldOffset = worldOffset;
        }
    }
}