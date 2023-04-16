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
        private IMapSelector _builtSelector;

        public IMapSelector Create()
        {
            if (_builtSelector != null)
                return _builtSelector;

            _builtSelector = new MapSelector(_mapFactories.Select(factory => factory.Create()).ToList());
            return _builtSelector;
        }
    }
}