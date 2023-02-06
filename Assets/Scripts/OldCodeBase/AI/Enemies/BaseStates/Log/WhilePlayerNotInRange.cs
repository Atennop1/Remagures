using Remagures.AI.StateMachine;
using UnityEngine;

namespace Remagures.AI.Enemies 
{
    public class WhilePlayerNotInRange : IState
    {
        private readonly IEnemyWithTarget _enemyWithTarget;
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public WhilePlayerNotInRange(IEnemyWithTarget enemyWithTarget)
            => _enemyWithTarget = enemyWithTarget;

            public void OnEnter()
        {
            _enemyWithTarget.Movement.StopMoving();
            var enemyAnimator = _enemyWithTarget.Animations.Animator;
            
            enemyAnimator.SetBool(WAKE_UP_ANIMATOR_NAME, false);
            enemyAnimator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
        }

        public void OnExit() { }
        public void Tick() { }
    }
}