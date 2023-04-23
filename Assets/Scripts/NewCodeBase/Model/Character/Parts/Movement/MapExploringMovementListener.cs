using System;
using Remagures.Model.MapSystem;
using UnityEngine;

namespace Remagures.Model.Character
{
    public class MapExploringMovementListener : ICharacterMovement
    {
        public Transform Transform => _characterMovement.Transform;
        public Vector2 CharacterLookDirection => _characterMovement.CharacterLookDirection;
        public bool IsMoving => _characterMovement.IsMoving;
        
        private readonly ICharacterMovement _characterMovement;
        private readonly IMapExplorer _mapExplorer;

        public MapExploringMovementListener(ICharacterMovement characterMovement, IMapExplorer mapExplorer)
        {
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
            _mapExplorer = mapExplorer ?? throw new ArgumentNullException(nameof(mapExplorer));
        }

        public void MoveTo(Vector3 endPosition)
        {
            _mapExplorer.Explore();
            _characterMovement.MoveTo(endPosition);
        }

        public void StopMoving() 
            => _characterMovement.StopMoving();
    }
}