using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public interface IMarkers
    {
        IMarker CharacterMarker { get; }
        IReadOnlyList<IMarker> GoalMarkers { get; }
    }
}