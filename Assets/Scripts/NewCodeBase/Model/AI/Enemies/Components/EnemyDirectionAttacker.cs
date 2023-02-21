using Cysharp.Threading.Tasks;
using Remagures.Factories;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class EnemyDirectionAttacker
    {
        private readonly int _fireDelayInMilliseconds;
        private bool _canFire = true;

        private readonly ProjectileFactory _projectileFactory;
        
        public void Attack(Vector2 direction)
        {
            if (!_canFire) 
                return;

            var projectile = _projectileFactory.Create(Quaternion.identity);
            projectile.Launch(direction);
            _canFire = false;
        }

        private async void ReloadingTask()
        {
            _canFire = false;
            await UniTask.Delay(_fireDelayInMilliseconds);
            _canFire = true;
        }
    }
}