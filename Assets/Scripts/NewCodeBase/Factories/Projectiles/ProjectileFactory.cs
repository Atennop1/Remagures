﻿using Remagures.Model;
using UnityEngine;

namespace Remagures.Factories
{
    public class ProjectileFactory : MonoBehaviour, IProjectileFactory
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;

        public IProjectile Create(Quaternion rotation)
        {
            var projectileGameObject = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
            var projectile = projectileGameObject.GetComponent<IProjectile>();
            
            projectileGameObject.transform.rotation = rotation;
            return projectile;
            
            //projectile.GetComponent<CharacterAttack>().Init(_player.PlayerData.UniqueSetup); //TODO remove this after making composite layer
        }
    }
}