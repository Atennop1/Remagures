using Remagures.Model.Projectiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ProjectileFactory : SerializedMonoBehaviour, IProjectileFactory
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        private IProjectile _builtProjectile;
        
        public IProjectile Create()
        {
            if (_builtProjectile != null)
                return _builtProjectile;

            _builtProjectile = new Projectile(_rigidbody, _speed);
            return _builtProjectile;
        }
    }
}