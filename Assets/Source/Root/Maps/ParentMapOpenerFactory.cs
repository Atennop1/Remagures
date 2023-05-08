using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ParentMapOpenerFactory : SerializedMonoBehaviour, IParentMapOpenerFactory
    {
        [SerializeField] private List<IMapFactory> _mapFactories;
        [SerializeField] private IMapSelectorFactory _mapSelectorFactory;
        private ParentMapOpener _builtOpener;

        public ParentMapOpener Create()
        {
            if (_builtOpener != null)
                return _builtOpener;

            _builtOpener = new ParentMapOpener(_mapFactories.Select(factory => factory.Create()).ToList(), _mapSelectorFactory.Create());
            return _builtOpener;
        }
    }
}