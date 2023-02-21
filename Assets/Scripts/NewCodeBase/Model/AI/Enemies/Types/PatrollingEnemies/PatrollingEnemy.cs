using Remagures.Model.Health;
using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;
using SM = Remagures.Model.AI.StateMachine;

namespace Remagures.Model.AI.Enemies.PatrollingEnemies
{
    public sealed class PatrollingEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public SM StateMachine => _enemyWithTarget.StateMachine;

        public IHealth Health => _enemyWithTarget.Health;
        public IEnemyAnimations Animations => _enemyWithTarget.Animations;
        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;

        private void Start()
        {
            IState playerNotInRangeState = new WhilePlayerNotInRange(this);
            IState moveToPlayerState = new MoveToPlayer(this);
            IState attackPlayerState = new AttackPlayer(this);
            IState knockedState = new KnockedState(this);
            
            StateMachine.AddTransition(attackPlayerState, moveToPlayerState, () => SeePlayer() && !PlayerTooNear());
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

        private void Update() 
            => StateMachine.Tick();
        
        private bool PlayerTooNear() 
            => Vector3.Distance(TargetData.Transform.position, transform.position) <= TargetData.AttackRadius;
        
        private bool SeePlayer() 
            => Vector3.Distance(TargetData.Transform.position, transform.position) <= TargetData.ChaseRadius;
        
        private bool PlayerTooFar() 
            => Vector3.Distance(TargetData.Transform.position, transform.position) > TargetData.ChaseRadius;
    }
}
