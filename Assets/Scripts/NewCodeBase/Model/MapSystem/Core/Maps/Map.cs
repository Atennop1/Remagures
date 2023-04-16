using System;
using System.Collections.Generic;
using Remagures.Root;
using Remagures.View.MapSystem;

namespace Remagures.Model.MapSystem
{
    public sealed class Map : IMap, ILateUpdatable
    {
        public bool HasOpened { get; private set; }
        public IMarkers Markers { get; }
        public IReadOnlyList<IMapTransition> Transitions { get; }
        
        private readonly IMapView _mapView;
        private readonly IIsMapVisited _isMapVisited;
            
        public Map(IMapView mapView, IMarkers markers, List<IMapTransition> transitions, IIsMapVisited isMapVisited)
        {
            _mapView = mapView ?? throw new ArgumentNullException(nameof(mapView));
            Markers = markers ?? throw new ArgumentNullException(nameof(markers));
            Transitions = transitions ?? throw new ArgumentNullException(nameof(transitions));
            _isMapVisited = isMapVisited ?? throw new ArgumentNullException(nameof(isMapVisited));
        }
        
        public void LateUpdate()
            => HasOpened = false;
        
        public void Open()
        {
            if (IsVisited())
            {
                HasOpened = true;
                _mapView.Display();
                return;
            }
            
            _mapView.DisplayFailure();
        }
        
        public bool CanOpen()
            => IsVisited();

        private bool IsVisited()
            => _isMapVisited.Get();
    }
}
