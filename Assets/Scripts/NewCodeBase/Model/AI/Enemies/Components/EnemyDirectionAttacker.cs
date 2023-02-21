using System;
using Cysharp.Threading.Tasks;
using Remagures.Factories;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    sealed class EnemyDirectionAttacker
    {
        private readonly int _fireDelayInMilliseconds;
        private bool _canFire = true;

        private readonly ProjectileFactory _projectileFactory;

        public EnemyDirectionAttacker(ProjectileFactory projectileFactory)
            => _projectileFactory = projectileFactory ?? throw new ArgumentNullException(nameof(projectileFactory));
        

        public async void Attack(Vector2 direction)
        {
            if (!_canFire) 
                return;

            var projectile = _projectileFactory.Create(Quaternion.identity);
            projectile.Launch(direction);

            await ReloadingTask();
        }

        private async UniTask ReloadingTask()
        {
            _canFire = false;
            await UniTask.Delay(_fireDelayInMilliseconds);
            _canFire = true;
        }
    }
}