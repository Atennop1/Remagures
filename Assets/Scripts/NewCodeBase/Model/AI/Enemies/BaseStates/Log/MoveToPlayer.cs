using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies 
{
    public sealed class MoveToPlayer : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly EnemyTargetData _enemyTargetData;
        private readonly IEnemyMovementView _enemyMovementView;

        public MoveToPlayer(IEnemyMovement enemyMovement, EnemyTargetData enemyTargetData, IEnemyMovementView enemyMovementView)
        {
            _enemyTargetData = enemyTargetData;
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            _enemyMovement.Move(_enemyTargetData.Transform.position);
            _enemyMovementView.SetIsWakeUp(true);
            _enemyMovementView.SetIsStaying(false);
        }
        
        public void OnEnter() { }
        public void OnExit() { }
    }
}