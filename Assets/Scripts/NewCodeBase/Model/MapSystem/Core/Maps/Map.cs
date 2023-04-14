using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Plugins;
using Remagures.Root;
using Remagures.View.MapSystem;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class Map : IMap, ILateUpdatable
    {
        public bool HasOpened { get; private set; }
        public IMarkers Markers { get; }
        public IReadOnlyList<MapTransition> Transitions { get; }
        
        private readonly IMapView _mapView;
        private readonly IIsMapVisited _isMapVisited;
            
        public Map(IMarkers markers, List<MapTransition> transitions, IIsMapVisited isMapVisited)
        {
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
