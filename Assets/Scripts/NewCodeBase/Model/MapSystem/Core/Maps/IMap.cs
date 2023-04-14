using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public interface IMap
    {
        bool HasOpened { get; }
        bool CanOpen();
        void Open();

        IMarkers Markers { get; }
        IReadOnlyList<MapTransition> Transitions { get; }
    }
}