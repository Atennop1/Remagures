using System;
using Remagures.Model.Health;
using Remagures.Root;

namespace Remagures.Model.AI.Enemies
{
    public class UniversalEnemyWithTarget : IEnemyWithTarget, IUpdatable
    {
        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public IHealth Health => _enemyWithTarget.Health;
        
        public IEnemyAnimations Animations => _enemyWithTarget.Animations;
        public StateMachine StateMachine { get; }

        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;
        
        private readonly IEnemyWithTarget _enemyWithTarget;

        public UniversalEnemyWithTarget(IEnemyWithTarget enemyWithTarget, IEnemyStateMachineBuilder enemyStateMachineBuilder)
        {
            _enemyWithTarget = enemyWithTarget ?? throw new ArgumentNullException(nameof(enemyWithTarget));
            StateMachine = enemyStateMachineBuilder.Build();
        }

        public void Update()
            => StateMachine.Tick();
    }
}