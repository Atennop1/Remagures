using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies 
{
    public class WhilePlayerNotInRange : IState
    {
        private readonly IEnemyWithTarget _enemyWithTarget;
        private readonly IEnemyMovementView _enemyMovementView;

        public WhilePlayerNotInRange(IEnemyWithTarget enemyWithTarget, IEnemyMovementView enemyMovementView)
        {
            _enemyWithTarget = enemyWithTarget ?? throw new ArgumentNullException(nameof(enemyWithTarget));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void OnEnter()
        {
            _enemyWithTarget.Movement.StopMoving();
            _enemyMovementView.SetIsWakeUp(false);
            _enemyMovementView.SetIsStaying(false);
        }

        public void OnExit() { }
        public void Update() { }
    }
}