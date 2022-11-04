using Remagures.AI.Enemies.Abstraction;
using Remagures.AI.Enemies.BaseStates;
using Remagures.AI.Enemies.Components;
using Remagures.Components.Base;
using Remagures.AI.Enemies.BaseStates.Log;
using Sirenix.OdinInspector;
using SM = Remagures.AI.StateMachine;

using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.AI.Enemies.Types.HauntingEnemys
{
    public class HauntingEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public Health Health => _enemyWithTarget.Health;
        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public StateMachine.StateMachine StateMachine => _enemyWithTarget.StateMachine;
        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;
        
        private void Start()
        {
            SM.IState playerNotInRangeState = new WhilePlayerNotInRange(this);
            SM.IState moveToPlayerState = new MoveToPlayer(this);
            SM.IState attackPlayerState = new AttackPlayer(this);
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

        private void FixedUpdate() => StateMachine.Tick();
        private bool PlayerTooNear() => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.AttackRadius;
        private bool SeePlayer() => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.ChaseRadius;
        private bool PlayerTooFar() => Vector3.Distance(TargetData.Target.position, transform.position) >= TargetData.ChaseRadius;
    }
}