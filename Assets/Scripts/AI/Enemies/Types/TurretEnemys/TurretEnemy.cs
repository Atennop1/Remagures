using Remagures.AI.Enemies.Abstraction;
using Remagures.AI.Enemies.BaseStates;
using Remagures.AI.Enemies.Components;
using Remagures.AI.Enemies.Types.TurretEnemys.States;
using Remagures.Components.Base;
using Remagures.Components.Projectiles;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using SM = Remagures.AI.StateMachine;
using AttackPlayer = Remagures.AI.Enemies.Types.TurretEnemys.States.AttackPlayer;

namespace Remagures.AI.Enemies.Types.TurretEnemys
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
        public StateMachine.StateMachine StateMachine => _enemyWithTarget.StateMachine;
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
            SM.IState playerNotInRangeState = new WhilePlayerNotInRange(this);
            SM.IState attackPlayerState = new AttackPlayer(this);
            SM.IState knockedState = new KnockedState(this);
            
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
