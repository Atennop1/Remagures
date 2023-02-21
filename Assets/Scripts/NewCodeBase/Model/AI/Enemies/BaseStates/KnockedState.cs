using System;

namespace Remagures.Model.AI.Enemies
{
    public class KnockedState : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly IEnemyAnimations _enemyAnimations;

        public KnockedState(IEnemyMovement enemyMovement, IEnemyAnimations enemyAnimations)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyAnimations = enemyAnimations ?? throw new ArgumentNullException(nameof(enemyAnimations));
        }

        public void OnEnter()
        {
            _enemyMovement.StopMoving();
            _enemyAnimations.SetIsKnocked(true);
        }

        public void Update() { }
        public void OnExit() { }
    }
}