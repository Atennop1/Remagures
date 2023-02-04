using Remagures.AI.StateMachine;
using UnityEngine;

namespace Remagures.AI.Enemies 
{
    public class MoveToPlayer : IState
    {
        private readonly IEnemyWithTarget _enemyWithTarget;
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public MoveToPlayer(IEnemyWithTarget enemyWithTarget)
            => _enemyWithTarget = enemyWithTarget;

        public void OnEnter() { }
        public void OnExit() { }

        public void Tick()
        {
            _enemyWithTarget.Movement.Move(_enemyWithTarget.TargetData.Target);
            var enemyAnimator = _enemyWithTarget.Animations.Animator;
            
            enemyAnimator.SetBool(WAKE_UP_ANIMATOR_NAME, true);
            enemyAnimator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
        }
    }
}