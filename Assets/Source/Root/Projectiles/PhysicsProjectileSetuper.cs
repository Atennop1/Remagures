using Remagures.Model.Projectiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsProjectileSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IProjectileFactory _projectileFactory;
        [SerializeField] private PhysicsProjectile _physicsProjectile;

        private void Awake() 
        => _physicsProjectile.Construct(_projectileFactory.Create());
    }
}