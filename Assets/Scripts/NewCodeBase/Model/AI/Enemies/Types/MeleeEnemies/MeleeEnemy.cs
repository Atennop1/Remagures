using System.Collections;
using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;
using SM = Remagures.Model.AI.StateMachine;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class MeleeEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public Health.Health Health => _enemyWithTarget.Health;

        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public SM StateMachine => _enemyWithTarget.StateMachine;

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
