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
        public IReadOnlyList<IMarker> Markers { get; }
        public IReadOnlyList<MapTransition> Transitions { get; }

        private readonly IReadOnlyList<SceneReference> _scenes;
        private readonly IMapView _mapView;
            
        public Map(IReadOnlyList<SceneReference> scenes, List<IMarker> markers, List<MapTransition> transitions)
        {
            _scenes = scenes ?? throw new ArgumentException(nameof(scenes));
            Markers = markers ?? throw new ArgumentNullException(nameof(markers));
            Transitions = transitions ?? throw new ArgumentNullException(nameof(transitions));
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
            => _scenes.Count == 0 || _scenes.Any(scene => PlayerPrefs.HasKey("Visited" + scene.ScenePath));
    }
}
