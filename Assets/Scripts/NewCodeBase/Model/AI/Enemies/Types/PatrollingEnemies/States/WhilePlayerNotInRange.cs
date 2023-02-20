using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.PatrollingEnemies
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly PatrollingEnemy _patrollingEnemy;
        private readonly IEnemyMovementView _enemyMovementView;
        
        public WhilePlayerNotInRange(PatrollingEnemy patrollingEnemy, IEnemyMovementView enemyMovementView)
        {
            _patrollingEnemy = patrollingEnemy ?? throw new ArgumentNullException(nameof(patrollingEnemy));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            _patrollingEnemy.Movement.Move(_patrollingEnemy.CurrentPointTransform.position);
            _enemyMovementView.SetIsStaying(false);
        }

        public void OnEnter() { }
        public void OnExit() { }
    }
}
