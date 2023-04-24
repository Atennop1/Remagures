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
        private readonly ICharacterMarkerView _markerView;
        private readonly CharacterPositionOnMap _characterPositionOnMap;

        public CharacterMarker(SceneData[] mapScenes, ICharacterMarkerView markerView, CharacterPositionOnMap characterPositionOnMap)
        {
            _mapScenes = mapScenes ?? throw new ArgumentNullException(nameof(mapScenes));
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _characterPositionOnMap = characterPositionOnMap ?? throw new ArgumentNullException(nameof(characterPositionOnMap));
        }

        public void Enable()
        {
            _markerView.UnDisplay();
            
            if (IsActive())
                _markerView.Display(_characterPositionOnMap.Get());
        }

        public bool IsActive()
        {
            var currentScenePath = SceneManager.GetActiveScene().path;
            return _mapScenes.Any(scene => scene.Path == currentScenePath);
        }
    }
}
