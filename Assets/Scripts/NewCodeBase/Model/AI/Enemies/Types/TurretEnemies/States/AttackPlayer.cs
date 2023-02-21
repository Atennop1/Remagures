using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.TurretEnemies
{
    public sealed class AttackPlayer : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly EnemyTargetData _enemyTargetData;
        private readonly EnemyDirectionAttacker _enemyAttacker;
        private readonly IEnemyMovementView _enemyMovementView;

        public AttackPlayer(IEnemyMovement enemyMovement, EnemyTargetData enemyTargetData, EnemyDirectionAttacker enemyAttacker, IEnemyMovementView enemyMovementView)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyTargetData = enemyTargetData;
            _enemyAttacker = enemyAttacker ?? throw new ArgumentNullException(nameof(enemyAttacker));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            var direction = _enemyTargetData.Transform.transform.position - _enemyMovement.Transform.position;
            _enemyAttacker.Attack(direction);
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