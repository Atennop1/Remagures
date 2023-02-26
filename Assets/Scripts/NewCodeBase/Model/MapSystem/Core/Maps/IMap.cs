using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public interface IMap
    {
        void Open();
        bool CanOpen();
        bool HasOpened { get; }
        
        IMarkers Markers { get; }
        IReadOnlyList<MapTransition> Transitions { get; }
    }
}