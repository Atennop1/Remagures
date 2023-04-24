using System.Linq;

namespace Remagures.Model.MapSystem
{
    public sealed class IsGoalMarkerContainsInMap : IIsMarkerContainsInMap
    {
        public bool Get(IMap map)
            => map.Markers.GoalMarkers.Any(marker => marker.IsActive());
    }
}