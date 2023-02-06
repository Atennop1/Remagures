using Remagures.AI.StateMachine;
using UnityEngine;

namespace Remagures.AI.Enemies 
{
    public sealed class AttackPlayer : IState
    {
        private readonly IEnemyWithTarget _enemyWithTarget;
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public AttackPlayer(IEnemyWithTarget enemyWithTarget)
            => _enemyWithTarget = enemyWithTarget;

        public void Tick()
        {
            var enemyAnimator = _enemyWithTarget.Animations.Animator;
            var temp = Vector3.MoveTowards(enemyAnimator.transform.position, _enemyWithTarget.TargetData.Target.position, 1);
            _enemyWithTarget.Animations.ChangeAnim(temp - enemyAnimator.transform.position, enemyAnimator);
        }

        public void OnEnter()
        {
            _enemyWithTarget.Movement.StopMoving();
            _enemyWithTarget.Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
        }

        public void OnExit() { }
    }
}