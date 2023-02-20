using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.TurretEnemies
{
    public sealed class AttackPlayer : IState
    {
        private readonly TurretEnemy _turretEnemy;
        private readonly IEnemyMovementView _enemyMovementView;

        public AttackPlayer(TurretEnemy turretEnemy, IEnemyMovementView enemyMovementView)
        {
            _turretEnemy = turretEnemy ?? throw new ArgumentNullException(nameof(turretEnemy));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            var direction = _turretEnemy.TargetData.Transform.transform.position - _turretEnemy.transform.position;
            _turretEnemy.InstantiateProjectile(direction);
            _enemyMovementView.SetAnimationsVector(direction);
        }

        public void OnEnter()
        {
            _enemyMovementView.SetIsWakeUp(true);
            _enemyMovementView.SetIsStaying(true);
        }
        
        public void OnExit() { }
    }
}