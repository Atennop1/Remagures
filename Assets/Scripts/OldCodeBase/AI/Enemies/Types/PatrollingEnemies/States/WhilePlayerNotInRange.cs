using Remagures.AI.StateMachine;
using UnityEngine;

namespace Remagures.AI.Enemies.PatrollingEnemies
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly PatrollingEnemy _patrollingEnemy;
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");
        
        public WhilePlayerNotInRange(PatrollingEnemy patrollingEnemy)
            => _patrollingEnemy = patrollingEnemy;

        public void Update()
        {
            _patrollingEnemy.Movement.Move(_patrollingEnemy.CurrentPointTransform);
            _patrollingEnemy.Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
        }

        public void OnEnter() { }
        public void OnExit() { }
    }
}
