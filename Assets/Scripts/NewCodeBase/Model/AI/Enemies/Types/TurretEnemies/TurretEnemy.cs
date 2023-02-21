using System;
using Remagures.Model.Health;
using Remagures.Model.Knockback;
using Remagures.Root;
using Remagures.View.Enemies;
using UnityEngine;
using SM = Remagures.Model.AI.StateMachine;

namespace Remagures.Model.AI.Enemies.TurretEnemies
{
    public sealed class TurretEnemy : IEnemyWithTarget, IUpdatable
    {
        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public IHealth Health => _enemyWithTarget.Health;
        
        public IEnemyAnimations Animations => _enemyWithTarget.Animations;
        public SM StateMachine => _enemyWithTarget.StateMachine;
        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;
        
        private readonly IEnemyWithTarget _enemyWithTarget;

        public void Update()
            => StateMachine.Tick();

        private TurretEnemy(IEnemyWithTarget enemyWithTarget, IEnemyMovementView enemyMovementView, EnemyDirectionAttacker enemyAttacker)
        {
            _enemyWithTarget = enemyWithTarget ?? throw new ArgumentNullException(nameof(enemyWithTarget));

            if (enemyMovementView == null)
                throw new ArgumentNullException(nameof(enemyMovementView));

            if (enemyAttacker == null)
                throw new ArgumentNullException(nameof(enemyAttacker));
            
            IState playerNotInRangeState = new WhilePlayerNotInRange(enemyMovementView);
            IState attackPlayerState = new AttackPlayer(Movement, TargetData, enemyAttacker, enemyMovementView);
            IState knockedState = new KnockedState(Movement, Animations);
            
            StateMachine.AddTransition(attackPlayerState, playerNotInRangeState, PlayerTooFar);
            StateMachine.AddTransition(playerNotInRangeState, attackPlayerState, SeePlayer);

            if (TryGetComponent(out IKnockable knockable))
            {
                StateMachine.AddTransition(knockedState, attackPlayerState, () => !knockable.IsKnocking);
                StateMachine.AddUniversalTransition(knockedState, () => knockable.IsKnocking);
            }

            StateMachine.SetState(playerNotInRangeState);
        }

        private bool PlayerTooFar() 
            => Vector3.Distance(TargetData.Transform.position, Movement.Transform.position) > TargetData.ChaseRadius;
        
        private bool SeePlayer() 
            => Vector3.Distance(TargetData.Transform.position, Movement.Transform.position) <= TargetData.ChaseRadius;
    }
}
