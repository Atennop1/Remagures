using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class MapExplorer : IMapExplorer
    {
        private readonly CharacterPositionOnMap _characterPositionOnMap;
        private readonly Texture2D _currentMapFogTexture;

        public MapExplorer(IMapSelector mapSelector, CharacterPositionOnMap characterPositionOnMap, FogsOfWar fogsOfWar)
        {
            if (mapSelector == null) 
                throw new ArgumentNullException(nameof(mapSelector));
            
            if (fogsOfWar == null)
                throw new ArgumentNullException(nameof(fogsOfWar));
            
            _characterPositionOnMap = characterPositionOnMap ?? throw new ArgumentNullException(nameof(characterPositionOnMap));
            _currentMapFogTexture = fogsOfWar.GetFor(mapSelector.CurrentLocationMap);
        }

        public void Explore()
        {
            var position = _characterPositionOnMap.Get();
            _currentMapFogTexture.DrawCircle(Color.black * 0.5f, position.x, position.y, 50);
            _currentMapFogTexture.DrawCircle(Color.clear, position.x, position.y, 47);
            _currentMapFogTexture.Apply();
        }
    }
}