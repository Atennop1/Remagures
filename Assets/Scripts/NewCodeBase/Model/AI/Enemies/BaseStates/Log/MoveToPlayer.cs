using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies 
{
    public class MoveToPlayer : IState
    {
        private readonly IEnemyWithTarget _enemyWithTarget;
        private readonly IEnemyMovementView _enemyMovementView;

        public MoveToPlayer(IEnemyWithTarget enemyWithTarget, IEnemyMovementView enemyMovementView)
        {
            _enemyWithTarget = enemyWithTarget ?? throw new ArgumentNullException(nameof(enemyWithTarget));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            _enemyWithTarget.Movement.Move(_enemyWithTarget.TargetData.Transform.position);
            _enemyMovementView.SetIsWakeUp(true);
            _enemyMovementView.SetIsStaying(false);
        }
        
        public void OnEnter() { }
        public void OnExit() { }
    }
}