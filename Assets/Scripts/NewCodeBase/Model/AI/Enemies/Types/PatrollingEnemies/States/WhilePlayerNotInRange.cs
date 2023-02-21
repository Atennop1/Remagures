using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.PatrollingEnemies
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly PatrolEnemyMovement _patrolEnemyMovement;
        private readonly IEnemyMovementView _enemyMovementView;
        
        public WhilePlayerNotInRange(PatrolEnemyMovement patrolEnemyMovement, IEnemyMovementView enemyMovementView)
        {
            _patrolEnemyMovement = patrolEnemyMovement ?? throw new ArgumentNullException(nameof(patrolEnemyMovement));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            _patrolEnemyMovement.Move(_patrolEnemyMovement.CurrentPointTransform.position);
            _enemyMovementView.SetIsStaying(false);
        }

        public void OnEnter()
            => _enemyMovementView.SetIsWakeUp(true);
        
        public void OnExit() { }
    }
}
