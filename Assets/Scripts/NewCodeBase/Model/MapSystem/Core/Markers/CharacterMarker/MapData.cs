using System;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public readonly struct MapData
    {
        public readonly Texture2D MapTexture;
        public readonly Vector2 WorldSize;
        public readonly Vector2 WorldOffset;

        public MapData(Texture2D mapTexture, Vector2 worldSize, Vector2 worldOffset)
        {
            MapTexture = mapTexture ?? throw new ArgumentNullException(nameof(mapTexture));
            WorldSize = worldSize;
            WorldOffset = worldOffset;
        }
    }
}