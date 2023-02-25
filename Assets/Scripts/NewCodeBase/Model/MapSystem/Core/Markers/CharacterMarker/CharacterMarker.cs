using System;
using System.Linq;
using Remagures.Plugins;
using Remagures.View.MapSystem;
using UnityEngine.SceneManagement;

namespace Remagures.Model.MapSystem
{
    public sealed class CharacterMarker : IMarker
    {
        private readonly SceneReference[] _mapScenes;
        private readonly IMarkerView _markerView;
        private readonly CharacterPositionOnMapCalculator _characterPositionOnMapCalculator;

        public CharacterMarker(SceneReference[] mapScenes, IMarkerView markerView, CharacterPositionOnMapCalculator characterPositionOnMapCalculator)
        {
            _mapScenes = mapScenes ?? throw new ArgumentNullException(nameof(mapScenes));
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _characterPositionOnMapCalculator = characterPositionOnMapCalculator ?? throw new ArgumentNullException(nameof(characterPositionOnMapCalculator));
        }

        public bool IsActive()
        {
            _markerView.UnDisplay();
            var currentScenePath = SceneManager.GetActiveScene().path;

            if (_mapScenes.Any(scene => scene.ScenePath == currentScenePath))
                return false;
                
            _markerView.Display(_characterPositionOnMapCalculator.Calculate());
            return true;
        }
    }
}
