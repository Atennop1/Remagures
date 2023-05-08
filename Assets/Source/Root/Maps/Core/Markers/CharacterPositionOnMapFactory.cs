using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class CharacterPositionOnMapFactory : SerializedMonoBehaviour, ICharacterPositionOnMapFactory
    {
        [SerializeField] private IMapData _mapData;
        [SerializeField] private Transform _characterTransform;
        private CharacterPositionOnMap _builtPosition;
        
        public CharacterPositionOnMap Create()
        {
            if (_builtPosition != null)
                return _builtPosition;

            _builtPosition = new CharacterPositionOnMap(_mapData, _characterTransform);
            return _builtPosition;
        }
    }
}