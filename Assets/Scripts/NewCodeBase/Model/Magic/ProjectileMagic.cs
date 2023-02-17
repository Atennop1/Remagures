using System;
using Remagures.Factories;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.Magic
{
    public class ProjectileMagic : IMagic
    {
        public MagicData Data { get; }
        
        private readonly IProjectileFactory _projectileFactory;
        private readonly CharacterMovement _characterMovement;

        public ProjectileMagic(MagicData magicData, IProjectileFactory projectileFactory, CharacterMovement characterMovement)
        {
            _projectileFactory = projectileFactory ?? throw new ArgumentNullException(nameof(projectileFactory));
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
            Data = magicData;
        }

        public void Activate()
        {
            var throwDirection = new Vector2(_characterMovement.CharacterLookDirection.x, _characterMovement.CharacterLookDirection.y);
            var arrowDirection = new Vector3(0, 0, Mathf.Atan2(_characterMovement.CharacterLookDirection.y, _characterMovement.CharacterLookDirection.x) * Mathf.Rad2Deg);
            
            var projectile = _projectileFactory.Create(Quaternion.Euler(arrowDirection));
            projectile.Launch(throwDirection);
        }
    }
}
