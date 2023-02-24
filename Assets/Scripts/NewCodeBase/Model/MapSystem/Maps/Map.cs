using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Plugins;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class Map : IMap
    {
        public IReadOnlyList<IMarker> Markers { get; }
        public IReadOnlyList<MapTransition> Transitions { get; }
        
        private readonly IReadOnlyList<SceneReference> _scenes;

        public Map(IReadOnlyList<SceneReference> scenes, List<IMarker> markers, List<MapTransition> transitions)
        {
            _scenes = scenes ?? throw new ArgumentException(nameof(scenes));
            Markers = markers ?? throw new ArgumentNullException(nameof(markers));
            Transitions = transitions ?? throw new ArgumentNullException(nameof(transitions));
        }
        
        public bool IsVisited 
            => _scenes.Count == 0 || _scenes.Any(scene => PlayerPrefs.HasKey("Visited" + scene.ScenePath));
    }
}
