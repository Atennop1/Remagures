using System;
using System.Linq;
using Remagures.Tools;
using Remagures.View.MapSystem;
using UnityEngine.SceneManagement;

namespace Remagures.Model.MapSystem
{
    public sealed class CharacterMarker : IMarker
    {
        private readonly SceneData[] _mapScenes;
        private readonly IMarkerView _markerView;
        private readonly CharacterPositionOnMap _characterPositionOnMap;

        public CharacterMarker(SceneData[] mapScenes, IMarkerView markerView, CharacterPositionOnMap characterPositionOnMap)
        {
            _mapScenes = mapScenes ?? throw new ArgumentNullException(nameof(mapScenes));
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _characterPositionOnMap = characterPositionOnMap ?? throw new ArgumentNullException(nameof(characterPositionOnMap));
        }

        public bool IsActive()
        {
            _markerView.UnDisplay();
            var currentScenePath = SceneManager.GetActiveScene().path;

            if (_mapScenes.Any(scene => scene.Path == currentScenePath))
                return false;
                
            _markerView.Display(_characterPositionOnMap.Get());
            return true;
        }
    }
}
