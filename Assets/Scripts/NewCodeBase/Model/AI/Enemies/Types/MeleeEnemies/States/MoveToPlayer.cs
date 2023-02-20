using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class MoveToPlayer : IState
    {
        private readonly MeleeEnemy _meleeEnemy;
        private readonly IEnemyMovementView _enemyMovementView;

        public MoveToPlayer(MeleeEnemy meleeEnemy, IEnemyMovementView enemyMovementView)
        {
            _meleeEnemy = meleeEnemy ?? throw new ArgumentNullException(nameof(meleeEnemy));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }
        
        public void Update()
        {
            _meleeEnemy.Movement.Move(_meleeEnemy.TargetData.Transform.position);
            _enemyMovementView.SetIsStaying(false);
        }
        
        public void OnEnter() { }
        public void OnExit() { }
    }
}