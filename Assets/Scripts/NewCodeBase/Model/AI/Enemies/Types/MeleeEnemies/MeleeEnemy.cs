using Remagures.Model.Health;
using Remagures.Model.Knockback;
using Remagures.Root;
using UnityEngine;
using SM = Remagures.Model.AI.StateMachine;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class MeleeEnemy : IEnemyWithTarget, IUpdatable
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public IHealth Health => _enemyWithTarget.Health;

        public IEnemyAnimations Animations => _enemyWithTarget.Animations;
        public SM StateMachine => _enemyWithTarget.StateMachine;

        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;

        private void Start()
        {
            IState playerNotInRangeState = new WhilePlayerNotInRange(this);
            IState moveToPlayerState = new MoveToPlayer(this);
            AttackPlayer attackPlayerState = new(this);
            IState knockedState = new KnockedState(this);
            
            StateMachine.AddTransition(attackPlayerState, moveToPlayerState, () => SeePlayer() && !PlayerTooNear() && !attackPlayerState.IsAttacking);
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

        public void Update() 
            => StateMachine.Tick();
        
        private bool PlayerTooNear() 
            => Vector3.Distance(TargetData.Transform.position, transform.position) <= TargetData.AttackRadius;
        
        private bool SeePlayer() 
            => Vector3.Distance(TargetData.Transform.position, transform.position) <= TargetData.ChaseRadius;
        
        private bool PlayerTooFar() 
            => Vector3.Distance(TargetData.Transform.position, transform.position) > TargetData.ChaseRadius;
    }
}
