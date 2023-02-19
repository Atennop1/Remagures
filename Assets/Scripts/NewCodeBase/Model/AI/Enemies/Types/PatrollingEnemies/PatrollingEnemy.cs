using Remagures.Model.AI.StateMachine;
using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;
using SM = Remagures.Model.AI.StateMachine.StateMachine;

namespace Remagures.Model.AI.Enemies.PatrollingEnemies
{
    public sealed class PatrollingEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;
        [SerializeField] private PatrolEnemyMovement _patrolEnemyMovement;
        
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        public Transform CurrentPointTransform => _patrolEnemyMovement.CurrentPointTransform;
        public IEnemyMovement Movement => _patrolEnemyMovement;
        public SM StateMachine => _enemyWithTarget.StateMachine;

        public Health.Health Health => _enemyWithTarget.Health;
        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;

        private void Start()
        {
            Animations.Animator.SetBool(WAKE_UP_ANIMATOR_NAME, true);

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
            => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.AttackRadius;
        
        private bool SeePlayer() 
            => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.ChaseRadius;
        
        private bool PlayerTooFar() 
            => Vector3.Distance(TargetData.Target.position, transform.position) > TargetData.ChaseRadius;
    }
}
