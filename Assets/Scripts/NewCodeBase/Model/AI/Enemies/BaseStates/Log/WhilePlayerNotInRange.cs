using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies 
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly IEnemyMovementView _enemyMovementView;

        public WhilePlayerNotInRange(IEnemyMovement enemyMovement, IEnemyMovementView enemyMovementView)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void OnEnter()
        {
            _enemyMovement.StopMoving();
            _enemyMovementView.SetIsWakeUp(false);
            _enemyMovementView.SetIsStaying(false);
        }

        public void OnExit() { }
        public void Update() { }
    }
}