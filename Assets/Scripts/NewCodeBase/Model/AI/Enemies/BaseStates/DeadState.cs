using System;

namespace Remagures.Model.AI.Enemies
{
    public sealed class DeadState : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly IEnemyAnimations _enemyAnimations;

        public DeadState(IEnemyMovement enemyMovement, IEnemyAnimations enemyAnimations)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyAnimations = enemyAnimations ?? throw new ArgumentNullException(nameof(enemyAnimations));
        }

        public void OnEnter()
        {
            _enemyMovement.StopMoving();
            _enemyAnimations.SetIsDead(true);
        }
        
        public void Update() { }
        public void OnExit() { }
    }
}