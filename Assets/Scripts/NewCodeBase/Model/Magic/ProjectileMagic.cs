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
        private readonly PlayerMovement _playerMovement;

        public ProjectileMagic(MagicData magicData, IProjectileFactory projectileFactory, PlayerMovement playerMovement)
        {
            _projectileFactory = projectileFactory ?? throw new ArgumentNullException(nameof(projectileFactory));
            _playerMovement = playerMovement ?? throw new ArgumentNullException(nameof(playerMovement));
            Data = magicData;
        }

        public void Activate()
        {
            var throwDirection = new Vector2(_playerMovement.PlayerViewDirection.x, _playerMovement.PlayerViewDirection.y);
            var arrowDirection = new Vector3(0, 0, Mathf.Atan2(_playerMovement.PlayerViewDirection.y, _playerMovement.PlayerViewDirection.x) * Mathf.Rad2Deg);
            
            var projectile = _projectileFactory.Create(Quaternion.Euler(arrowDirection));
            projectile.Launch(throwDirection);
        }
    }
}
