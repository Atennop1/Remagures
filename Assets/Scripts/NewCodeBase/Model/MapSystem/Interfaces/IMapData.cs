using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public interface IMapData
    {
        Texture2D MapTexture { get; }
        Vector2Int WorldSize { get; }
        Vector2Int WorldOffset { get; }
    }
}