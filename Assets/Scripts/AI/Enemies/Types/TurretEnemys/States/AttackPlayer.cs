using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.TurretEnemys.States
{
    public sealed class AttackPlayer : SM.IState
    {
        private readonly TurretEnemy _turretEnemy;
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public AttackPlayer(TurretEnemy turretEnemy)
        {
            _turretEnemy = turretEnemy;
        }

        public void Tick()
        {
            var distance = _turretEnemy.TargetData.Target.transform.position - _turretEnemy.transform.position;
            _turretEnemy.InstantiateProjectile(distance);

            var enemyAnimator = _turretEnemy.Animations.Animator;
            _turretEnemy.Animations.ChangeAnim(distance, enemyAnimator);
        }

        public void OnEnter()
        {
            var enemyAnimator = _turretEnemy.Animations.Animator;
            enemyAnimator.SetBool(WAKE_UP_ANIMATOR_NAME, true);
            enemyAnimator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
        }
        
        public void OnExit() { }
    }
}