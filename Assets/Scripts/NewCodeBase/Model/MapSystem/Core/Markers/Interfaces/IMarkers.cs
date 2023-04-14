using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public interface IMarkers
    {
        CharacterMarker CharacterMarker { get; }
        IReadOnlyList<GoalMarker> GoalMarkers { get; }
    }
}