using Remagures.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.TurretEnemies
{
    public sealed class TurretEnemy : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemyWithTarget _enemyWithTarget;
        [FormerlySerializedAs("Projectile")] [SerializeField] private Projectile _projectile;
        [SerializeField] private float _fireDelay;
        
        private bool _canFire = true;
        private float _fireDelaySeconds;

        public IEnemyMovement Movement => _enemyWithTarget.Movement;
        public Health Health => _enemyWithTarget.Health;
        public EnemyAnimations Animations => _enemyWithTarget.Animations;
        public SM StateMachine => _enemyWithTarget.StateMachine;
        public EnemyTargetData TargetData => _enemyWithTarget.TargetData;

        public void InstantiateProjectile(Vector3 spawnPoint)
        {
            if (!_canFire) return;
            
            var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
            projectile.Launch(spawnPoint);
            _canFire = false;
        }

        private void Update()
        {
            StateMachine.Tick();
            
            _fireDelaySeconds -= UnityEngine.Time.deltaTime;
            if (_fireDelaySeconds > 0) return;
        
            _canFire = true;
            _fireDelaySeconds = _fireDelay;
        }

        private void Start()
        {
            IState playerNotInRangeState = new States.WhilePlayerNotInRange(this);
            IState attackPlayerState = new States.AttackPlayer(this);
            IState knockedState = new KnockedState(this);
            
            StateMachine.AddTransition(attackPlayerState, playerNotInRangeState, PlayerTooFar);
            StateMachine.AddTransition(playerNotInRangeState, attackPlayerState, SeePlayer);

            if (TryGetComponent(out IKnockable knockable))
            {
                StateMachine.AddTransition(knockedState, attackPlayerState, () => !knockable.IsKnocked);
                StateMachine.AddAnyTransition(knockedState, () => knockable.IsKnocked);
            }

            StateMachine.SetState(playerNotInRangeState);
        }

        private bool PlayerTooFar() => Vector3.Distance(TargetData.Target.position, transform.position) >= TargetData.ChaseRadius;
        private bool SeePlayer() => Vector3.Distance(TargetData.Target.position, transform.position) <= TargetData.ChaseRadius && _canFire; //&& CurrentState != EnemyState.Stagger;
    }
}
