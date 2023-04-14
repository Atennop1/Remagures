using System;
using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public sealed class Markers : IMarkers
    {
        public IMarker CharacterMarker { get; }
        public IReadOnlyList<IMarker> GoalMarkers { get; }

        public Markers(IMarker characterMarker, IReadOnlyList<IMarker> goalMarkers)
        {
            CharacterMarker = characterMarker ?? throw new ArgumentNullException(nameof(characterMarker));
            GoalMarkers = goalMarkers ?? throw new ArgumentNullException(nameof(goalMarkers));
        }
    }
}