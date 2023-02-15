using System;
using Remagures.Factories;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.Magic
{
    public class ProjectileMagic : IMagic
    {
        public MagicData Data { get; }
        
        private readonly IProjectilesFactory _projectilesFactory;
        private readonly PlayerMovement _playerMovement;

        public ProjectileMagic(MagicData magicData, IProjectilesFactory projectilesFactory, PlayerMovement playerMovement)
        {
            _projectilesFactory = projectilesFactory ?? throw new ArgumentNullException(nameof(projectilesFactory));
            _playerMovement = playerMovement ?? throw new ArgumentNullException(nameof(playerMovement));
            Data = magicData;
        }

        public void Activate()
        {
            var throwDirection = new Vector2(_playerMovement.PlayerViewDirection.x, _playerMovement.PlayerViewDirection.y);
            var arrowDirection = new Vector3(0, 0, Mathf.Atan2(_playerMovement.PlayerViewDirection.y, _playerMovement.PlayerViewDirection.x) * Mathf.Rad2Deg);
            
            var projectile = _projectilesFactory.Create(Quaternion.Euler(arrowDirection));
            projectile.Launch(throwDirection);
        }
    }
}
