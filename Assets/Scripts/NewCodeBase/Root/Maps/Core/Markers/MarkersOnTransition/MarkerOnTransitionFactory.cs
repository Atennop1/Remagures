using Remagures.Model.MapSystem;
using Remagures.View.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MarkerOnTransitionFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IMapFactory _mapFactory;
        [SerializeField] private IMarkerView _markerView;
        [SerializeField] private IIsMarkerContainsInMapFactory _isMarkerContainsInMapFactory;
        private MarkerOnTransition _builtMarker;

        public MarkerOnTransition Create()
        {
            if (_builtMarker != null)
                return _builtMarker;

            _builtMarker = new MarkerOnTransition(_markerView, _mapFactory.Create(), _isMarkerContainsInMapFactory.Create());
            return _builtMarker;
        }
    }
}