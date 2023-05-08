using Remagures.Model.MapSystem;
using Remagures.View.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MarkerOnTransitionFactory : SerializedMonoBehaviour, IMarkerOnTransitionFactory
    {
        [SerializeField] private IMapFactory _mapFactory;
        [SerializeField] private IMarkerView _markerView;
        [SerializeField] private IIsMarkerContainsInMapFactory _isMarkerContainsInMapFactory;
        private IMarkerOnTransition _builtMarker;

        public IMarkerOnTransition Create()
        {
            if (_builtMarker != null)
                return _builtMarker;

            _builtMarker = new MarkerOnTransition(_markerView, _mapFactory.Create(), _isMarkerContainsInMapFactory.Create());
            return _builtMarker;
        }
    }
}