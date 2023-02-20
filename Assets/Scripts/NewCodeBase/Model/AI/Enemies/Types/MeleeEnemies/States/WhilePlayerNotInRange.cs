using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly MeleeEnemy _meleeEnemy;
        private readonly IEnemyMovementView _enemyMovementView;

        public WhilePlayerNotInRange(MeleeEnemy meleeEnemy, IEnemyMovementView enemyMovementView)
        {
            _meleeEnemy = meleeEnemy ?? throw new ArgumentNullException(nameof(meleeEnemy));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void OnEnter()
        {
            _meleeEnemy.Movement.StopMoving();
            _enemyMovementView.SetIsStaying(true);
        }

        public void OnExit() { }
        public void Update() { }
    }
}
