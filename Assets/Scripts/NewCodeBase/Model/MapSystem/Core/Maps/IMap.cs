using System.Collections.Generic;

namespace Remagures.Model.MapSystem
{
    public interface IMap
    {
        void Open();
        bool CanOpen();
        bool HasOpened { get; }
        
        IReadOnlyList<IMarker> Markers { get; }
        IReadOnlyList<MapTransition> Transitions { get; }
    }
}