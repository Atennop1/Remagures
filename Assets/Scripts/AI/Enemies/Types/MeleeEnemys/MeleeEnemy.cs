using System.Collections;
using Remagures.AI.Enemies.Abstraction;
using Remagures.AI.Enemies.BaseStates;
using Remagures.AI.Enemies.Components;
using Remagures.AI.Enemies.Types.MeleeEnemys.States;
using Remagures.Components.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.MeleeEnemys
{
    public sealed class MeleeEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public Health Health => _enemyWithTarget.Health;

        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public StateMachine.StateMachine StateMachine => _enemyWithTarget.StateMachine;

        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;
        
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");
        private readonly int ATTACKING_ANIMATOR_NAME = Animator.StringToHash("attacking");
        
        private Coroutine _attackCoroutine;

        public void StartAttackCoroutine()
        {
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
            Animations.Animator.SetBool(ATTACKING_ANIMATOR_NAME, true);

            yield return new WaitForSeconds(0.35f);

            Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
            Animations.Animator.SetBool(ATTACKING_ANIMATOR_NAME, false);
        }

        private void Start()
        {
            SM.IState playerNotInRangeState = new WhilePlayerNotInRange(this);
            SM.IState moveToPlayerState = new MoveToPlayer(this);
            AttackPlayer attackPlayerState = new(this);
            SM.IState knockedState = new KnockedState(this);
            
            StateMachine.AddTransition(attackPlayerState, moveToPlayerState, () => SeePlayer() && !PlayerTooNear() && !attackPlayerState.IsAttacking);
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
