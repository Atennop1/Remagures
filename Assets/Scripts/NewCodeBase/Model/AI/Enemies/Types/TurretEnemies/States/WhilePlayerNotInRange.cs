using System;
using Remagures.View.Enemies;

namespace Remagures.Model.AI.Enemies.TurretEnemies
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly IEnemyMovementView _enemyMovementView;

        public WhilePlayerNotInRange(IEnemyMovementView enemyMovementView)
            => _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));

        public void OnEnter()
        {
            _enemyMovementView.SetIsWakeUp(false);
            _enemyMovementView.SetIsStaying(true);
        }

        public void OnExit() { }
        public void Update() { }
    }
}