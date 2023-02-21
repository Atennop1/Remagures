using System;
using Remagures.Model.Health;
using Remagures.Model.Knockback;
using Remagures.Root;
using Remagures.View.Enemies;
using UnityEngine;
using SM = Remagures.Model.AI.StateMachine;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class MeleeEnemy : IEnemyWithTarget, IUpdatable
    {
        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public IHealth Health => _enemyWithTarget.Health;

        public IEnemyAnimations Animations => _enemyWithTarget.Animations;
        public SM StateMachine => _enemyWithTarget.StateMachine;

        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;
        
        private readonly IEnemyWithTarget _enemyWithTarget;

        public void Update() 
            => StateMachine.Tick();
        
        public MeleeEnemy(IEnemyWithTarget enemyWithTarget, IEnemyMovementView enemyMovementView, EnemyAttacker enemyAttacker)
        {
            _enemyWithTarget = enemyWithTarget ?? throw new ArgumentNullException(nameof(enemyWithTarget));
            
            if (enemyMovementView == null)
                throw new ArgumentNullException(nameof(enemyMovementView));
            
            IState playerNotInRangeState = new WhilePlayerNotInRange(Movement, enemyMovementView);
            IState moveToPlayerState = new MoveToPlayer(Movement, TargetData, enemyMovementView);
            IState attackPlayerState = new AttackPlayer(Movement, enemyAttacker);
            IState knockedState = new KnockedState(Movement, Animations);
            
            StateMachine.AddTransition(attackPlayerState, moveToPlayerState, () => SeePlayer() && !PlayerTooNear() && !enemyAttacker.IsAttacking);
            StateMachine.AddTransition(playerNotInRangeState, moveToPlayerState, SeePlayer);
            
            StateMachine.AddTransition(moveToPlayerState, attackPlayerState, PlayerTooNear);
            StateMachine.AddTransition(moveToPlayerState, playerNotInRangeState, PlayerTooFar);

            if (TryGetComponent(out IKnockable knockable))
            {
                StateMachine.AddTransition(knockedState, moveToPlayerState, () => !knockable.IsKnocking);
                StateMachine.AddUniversalTransition(knockedState, () => knockable.IsKnocking);
            }
            
            StateMachine.SetState(playerNotInRangeState);
        }

        private bool PlayerTooNear() 
            => Vector3.Distance(TargetData.Transform.position, Movement.Transform.position) <= TargetData.AttackRadius;
        
        private bool SeePlayer() 
            => Vector3.Distance(TargetData.Transform.position, Movement.Transform.position) <= TargetData.ChaseRadius;
        
        private bool PlayerTooFar() 
            => Vector3.Distance(TargetData.Transform.position, Movement.Transform.position) > TargetData.ChaseRadius;
    }
}
