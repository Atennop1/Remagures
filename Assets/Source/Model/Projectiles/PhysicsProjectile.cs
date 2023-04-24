using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Projectiles
{
    public sealed class PhysicsProjectile : SerializedMonoBehaviour, IProjectile
    {
        private IProjectile _projectile;

        public void Construct(IProjectile projectile)
            => _projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
        
        public void Launch(Vector2 direction) 
            => _projectile.Launch(direction);
    }
}