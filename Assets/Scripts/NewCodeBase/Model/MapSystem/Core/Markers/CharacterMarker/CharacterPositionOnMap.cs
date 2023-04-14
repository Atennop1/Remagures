using System;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class CharacterPositionOnMap
    {
        private readonly IMapData _mapData;
        private readonly Transform _characterTransform;

        public CharacterPositionOnMap(IMapData mapData, Transform characterTransform)
        {
            _mapData = mapData ?? throw new ArgumentNullException(nameof(mapData));
            _characterTransform = characterTransform ?? throw new ArgumentNullException(nameof(characterTransform));
        }

        public Vector2Int Get()
        {
            var characterIntPositionX = Mathf.RoundToInt(_characterTransform.position.x);
            var characterIntPositionY = Mathf.RoundToInt(_characterTransform.position.y);
            var characterIntPosition = new Vector2Int(characterIntPositionX, characterIntPositionY);
            
            return (characterIntPosition + _mapData.WorldSize / 2 + _mapData.WorldOffset) * 
                   new Vector2Int(_mapData.MapTexture.width / _mapData.WorldSize.x, _mapData.MapTexture.height / _mapData.WorldSize.y);
        }
    }
}