using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public interface IMap
    {
        bool IsVisited { get; }
        IReadOnlyList<IMarker> Markers { get; }
        IReadOnlyList<MapTransition> Transitions { get; }
    }
}