using Remagures.AI.Enemies.Abstraction;
using Remagures.AI.Enemies.BaseStates;
using Remagures.AI.Enemies.Components;
using Remagures.AI.Enemies.Components.Movement;
using Remagures.AI.Enemies.Types.PatrollingEnemys.States;
using Remagures.Components.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.PatrollingEnemys
{
    public sealed class PatrollingEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;
        [SerializeField] private PatrolEnemyMovement _patrolEnemyMovement;
        
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        public Transform CurrentPointTransform => _patrolEnemyMovement.CurrentPointTransform;
        public IEnemyMovement Movement => _patrolEnemyMovement;
        public SM.StateMachine StateMachine => _enemyWithTarget.StateMachine;

        public Health Health => _enemyWithTarget.Health;
        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;

        private void Start()
        {
            Animations.Animator.SetBool(WAKE_UP_ANIMATOR_NAME, true);

            SM.IState playerNotInRangeState = new WhilePlayerNotInRange(this);
            SM.IState moveToPlayerState = new MoveToPlayer(this);
            SM.IState attackPlayerState = new BaseStates.Log.AttackPlayer(this);
            SM.IState knockedState = new KnockedState(this);
            
            StateMachine.AddTransition(attackPlayerState, moveToPlayerState, () => SeePlayer() && !PlayerTooNear());
            StateMachine.AddTransition(playerNotInRangeState, moveToPlayerState, SeePlayer);
            
            StateMachine.AddTransition(moveToPlayerState, attackPlayerState, PlayerTooNear);
            StateMachine.AddTransition(moveToPlayerState, playerNotInRangeState, PlayerTooFar);

            if (TryGetComponent(out IKnockable knockable))
            {
                StateMachine.AddTransition(knockedState, moveToPlayerState, () => !knockable.IsKnocked);
                StateMachine.AddAnyTransition(knockedState, () => knockable.IsKnocked);
            }
            
            StateMachine.SetState(playerNotInRangeState);
        }

        private void Update() => StateMachine.Tick();
        private bool PlayerTooNear() => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.AttackRadius;
        private bool SeePlayer() => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.ChaseRadius;
        private bool PlayerTooFar() => Vector3.Distance(TargetData.Target.position, transform.position) > TargetData.ChaseRadius;
    }
}
