using System;
using System.Linq;
using Remagures.View.MapSystem;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public class GoalTransitionMarker : IMarker
    {
        private readonly IMarkerView _markerView;
        private readonly IMap _map;

        public GoalTransitionMarker(IMarkerView markerView, IMap map)
        {
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public bool IsActive()
        {
            _markerView.UnDisplay();
            
            if (!ContainsInMapTree(_map)) 
                return false;

            _markerView.Display(Vector2.zero);
            return true;
        }

        private bool ContainsInMapTree(IMap map)
            => ContainsInMap(map) || map.Transitions.Any(transition => ContainsInMapTree(transition.MapToOpen));
        
        private bool ContainsInMap(IMap map)
            => map.Markers.Select(marker => marker is GoalMarker).Any();
    }
}
