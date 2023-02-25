using System;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class CharacterPositionOnMapCalculator
    {
        private readonly MapData _mapData;
        private readonly Transform _characterTransform;

        public CharacterPositionOnMapCalculator(MapData mapData, Transform characterTransform)
        {
            _mapData = mapData;
            _characterTransform = characterTransform ?? throw new ArgumentNullException(nameof(characterTransform));
        }

        public Vector2 Calculate()
        {
            return (_characterTransform.position + (Vector3)(_mapData.WorldSize / 2 + _mapData.WorldOffset)) * 
                   (new Vector2(_mapData.MapTexture.width, _mapData.MapTexture.height) / _mapData.WorldSize);
        }
    }
}