using Remagures.Model.MapSystem;
using Remagures.Root;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class MapObjectFactory : SerializedMonoBehaviour, IMapObjectFactory
    {
        [SerializeField] private MapPrefabsFactory _mapPrefabsFactory;
        
        public GameObject Create(IMap map, Transform parent) 
            => Instantiate(_mapPrefabsFactory.Create().GetFor(map), parent);
    }
}