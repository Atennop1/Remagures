using System;
using Remagures.Model.Health;

namespace Remagures.Model.AI.Enemies
{
    public sealed class EnemyWithTarget : IEnemyWithTarget
    {
        public IEnemyMovement Movement => _enemy.Movement;
        public IHealth Health => _enemy.Health;
        
        public IEnemyAnimations Animations  => _enemy.Animations;
        public StateMachine StateMachine => _enemy.StateMachine;
        
        public EnemyTargetData TargetData { get; }
        private readonly IEnemy _enemy;

        public EnemyWithTarget(IEnemy enemy, EnemyTargetData targetData)
        {
            _enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
            TargetData = targetData;
        }
    }
}