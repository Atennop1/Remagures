using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapExplorerFactory : SerializedMonoBehaviour, IMapExplorerFactory
    {
        [SerializeField] private IMapSelectorFactory _mapSelectorFactory;
        [SerializeField] private ICharacterPositionOnMapFactory _characterPositionOnMapFactory;
        [SerializeField] private IFogsOfWarFactory fogsOfWarFactory;
        private IMapExplorer _builtMapExplorer;

        public IMapExplorer Create()
        {
            if (_builtMapExplorer != null)
                return _builtMapExplorer;

            _builtMapExplorer = new MapExplorer(_mapSelectorFactory.Create(), _characterPositionOnMapFactory.Create(), fogsOfWarFactory.Create());
            return _builtMapExplorer;
        }
    }
}