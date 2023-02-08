using Remagures.AI.StateMachine;
using Remagures.Components;
using Remagures.Model.Health;
using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using SM = Remagures.AI.StateMachine.StateMachine;

namespace Remagures.AI.Enemies.AreaEnemies
{
    public sealed class AreaEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;
        [FormerlySerializedAs("Boundary")] [SerializeField] private Collider2D _boundary;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public Health Health => _enemyWithTarget.Health;
        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public SM StateMachine => _enemyWithTarget.StateMachine;
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
                StateMachine.AddTransition(knockedState, moveToPlayerState, () => !knockable.IsKnocked);
                StateMachine.AddAnyTransition(knockedState, () => knockable.IsKnocked);
            }
            
            StateMachine.SetState(playerNotInRangeState);
        }

        private void Update() 
            => StateMachine.Tick();
        
        private bool PlayerTooNear() 
            => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.AttackRadius;
        
        private bool SeePlayer() 
            => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.ChaseRadius && _boundary.bounds.Contains(TargetData.Target.position);
        
        private bool PlayerTooFar() 
            => Vector3.Distance(TargetData.Target.position, transform.position) >= TargetData.ChaseRadius || !_boundary.bounds.Contains(TargetData.Target.position);
    }
}
