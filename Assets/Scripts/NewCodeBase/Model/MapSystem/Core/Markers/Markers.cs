using System;
using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public sealed class Markers : IMarkers
    {
        public CharacterMarker CharacterMarker { get; }
        public IReadOnlyList<GoalMarker> GoalMarkers { get; }

        public Markers(CharacterMarker characterMarker, IReadOnlyList<GoalMarker> goalMarkers)
        {
            CharacterMarker = characterMarker ?? throw new ArgumentNullException(nameof(characterMarker));
            GoalMarkers = goalMarkers ?? throw new ArgumentNullException(nameof(goalMarkers));
        }
    }
}