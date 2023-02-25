using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class MapExplorer
    {
        private readonly CharacterPositionOnMapCalculator _characterPositionOnMapCalculator;
        private readonly Texture2D _currentMapFogTexture;

        public MapExplorer(MapSelector mapSelector, CharacterPositionOnMapCalculator characterPositionOnMapCalculator, FogsOfWarContainer fogsOfWarContainer)
        {
            if (mapSelector == null) 
                throw new ArgumentNullException(nameof(mapSelector));
            
            if (fogsOfWarContainer == null)
                throw new ArgumentNullException(nameof(fogsOfWarContainer));
            
            _characterPositionOnMapCalculator = characterPositionOnMapCalculator ?? throw new ArgumentNullException(nameof(characterPositionOnMapCalculator));
            _currentMapFogTexture = fogsOfWarContainer.Get(mapSelector.CurrentLocationMap);
        }

        public void Explore()
        {
            var position = _characterPositionOnMapCalculator.Calculate();
            _currentMapFogTexture.DrawCircle(new Color(0, 0, 0, 0.5f), (int)position.x, (int)position.y, 50);
            _currentMapFogTexture.DrawCircle(new Color(0, 0, 0, 0), (int)position.x, (int)position.y, 47);
            _currentMapFogTexture.Apply();
        }
    }
}