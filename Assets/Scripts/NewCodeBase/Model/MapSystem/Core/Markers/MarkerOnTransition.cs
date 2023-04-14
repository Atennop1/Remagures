using System;
using System.Linq;
using Remagures.View.MapSystem;

namespace Remagures.Model.MapSystem
{
    public sealed class MarkerOnTransition
    {
        private readonly IMap _map;
        private readonly IMarkerView _markerView;
        private readonly IIsMarkerContainsInMap _isMarkerContainsInMap;

        public MarkerOnTransition(IMarkerView markerView, IMap map, IIsMarkerContainsInMap isMarkerContainsInMap)
        {
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _isMarkerContainsInMap = isMarkerContainsInMap ?? throw new ArgumentNullException(nameof(isMarkerContainsInMap));
        }

        public bool IsActive()
        {
            _markerView.UnDisplay();
            
            if (!ContainsInMapHierarchy(_map)) 
                return false;

            _markerView.Display();
            return true;
        }

        private bool ContainsInMapHierarchy(IMap map)
        {
            if (_isMarkerContainsInMap.Get(map))
                return true;
            
            return map.Transitions.Any(transition =>
            {
                var childMap = transition.MapToTransit;
                return _isMarkerContainsInMap.Get(childMap);
            });
        }
    }
}
