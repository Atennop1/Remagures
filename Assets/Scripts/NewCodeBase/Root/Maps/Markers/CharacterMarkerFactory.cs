using Remagures.Model.MapSystem;
using Remagures.Tools;
using Remagures.View.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class CharacterMarkerFactory : SerializedMonoBehaviour, IMarkerFactory
    {
        [SerializeField] private SceneData[] _mapScenes;
        [SerializeField] private ICharacterMarkerView _markerView;
        [SerializeField] private CharacterPositionOnMapFactory _characterPositionOnMapFactory;
        private IMarker _builtMarker;
        
        public IMarker Create()
        {
            if (_builtMarker != null)
                return _builtMarker;

            _builtMarker = new CharacterMarker(_mapScenes, _markerView, _characterPositionOnMapFactory.Create());
            return _builtMarker;
        }
    }
}