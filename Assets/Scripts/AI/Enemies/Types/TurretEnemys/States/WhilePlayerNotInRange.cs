using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.TurretEnemys.States
{
    public sealed class WhilePlayerNotInRange : SM.IState
    {
        private readonly TurretEnemy _turretEnemy;
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public WhilePlayerNotInRange(TurretEnemy turretEnemy)
        {
            _turretEnemy = turretEnemy;
        }

        public void OnEnter()
        {
            var enemyAnimator = _turretEnemy.Animations.Animator;
            enemyAnimator.SetBool(WAKE_UP_ANIMATOR_NAME, false);
            enemyAnimator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
        }

        public void OnExit() { }
        public void Tick() { }
    }
}