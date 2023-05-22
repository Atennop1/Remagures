using Remagures.Model.Projectiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class ProjectileFactory : SerializedMonoBehaviour, IProjectileFactory
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;

        public IProjectile Create(Quaternion rotation)
        {
            var projectileGameObject = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
            var projectile = projectileGameObject.GetComponent<IProjectile>();
            
            projectileGameObject.transform.rotation = rotation;
            return projectile;
        }
    }
}