using System;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class AttackPlayer : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly EnemyAttacker _enemyAttacker;

        public AttackPlayer(IEnemyMovement enemyMovement, EnemyAttacker enemyAttacker)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyAttacker = enemyAttacker ?? throw new ArgumentNullException(nameof(enemyAttacker));
        }

        public void OnEnter()
        {
            _enemyMovement.StopMoving();
            _enemyAttacker.Attack();
        }

        public void OnExit() { }
        public void Update() { }
    }
}