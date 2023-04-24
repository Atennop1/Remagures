using Remagures.Model.MapSystem;
using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "New MapData", menuName = "Map/Data", order = 0)]
    public sealed class MapDataSO : ScriptableObject, IMapData
    {
        [field: SerializeField] public Texture2D MapTexture { get; private set; }
        [field: SerializeField] public Vector2Int WorldSize { get; private set; }
        [field: SerializeField] public Vector2Int WorldOffset { get; private set; }
    }
}