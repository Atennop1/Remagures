using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapExplorerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private MapSelectorFactory _mapSelectorFactory;
        [SerializeField] private CharacterPositionOnMapFactory _characterPositionOnMapFactory;
        [SerializeField] private FogsOfWarFactory _fogsOfWarFactory;
        private IMapExplorer _builtMapExplorer;

        public IMapExplorer Create()
        {
            if (_builtMapExplorer != null)
                return _builtMapExplorer;

            _builtMapExplorer = new MapExplorer(_mapSelectorFactory.Create(), _characterPositionOnMapFactory.Create(), _fogsOfWarFactory.Create());
            return _builtMapExplorer;
        }
    }
}