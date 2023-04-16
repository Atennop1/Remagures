using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapSelectorFactory : SerializedMonoBehaviour
    {
        [SerializeField] private List<IMapFactory> _mapFactories;
        private MapSelector _builtSelector;

        public MapSelector Create()
        {
            if (_builtSelector != null)
                return _builtSelector;

            _builtSelector = new MapSelector(_mapFactories.Select(factory => factory.Create()).ToList());
            return _builtSelector;
        }
    }
}