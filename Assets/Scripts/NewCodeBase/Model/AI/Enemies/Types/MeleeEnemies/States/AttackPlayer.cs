using System;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class AttackPlayer : IState
    {
        private readonly MeleeEnemy _meleeEnemy;
        private readonly EnemyAttacker _enemyAttacker;

        public AttackPlayer(MeleeEnemy meleeEnemy)
            => _meleeEnemy = meleeEnemy ?? throw new ArgumentNullException(nameof(meleeEnemy));

        public void OnEnter()
        {
            _meleeEnemy.Movement.StopMoving();
            _enemyAttacker.Attack();
        }

        public void OnExit() { }
        public void Update() { }
    }
}