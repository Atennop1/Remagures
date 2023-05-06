using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapPrefabsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<IMapFactory, GameObject> _prefabs;
        private MapPrefabs _builtPrefabs;

        public MapPrefabs Create()
        {
            if (_builtPrefabs != null)
                return _builtPrefabs;

            _builtPrefabs = new MapPrefabs(_prefabs.ToDictionary(pair => pair.Key.Create(), pair => pair.Value));
            return _builtPrefabs;
        }
    }
}