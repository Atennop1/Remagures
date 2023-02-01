using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.PatrollingEnemys.States
{
    public sealed class MoveToPlayer : SM.IState
    {
        private readonly PatrollingEnemy _patrollingEnemy;
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public MoveToPlayer(PatrollingEnemy patrollingEnemy)
        {
            _patrollingEnemy = patrollingEnemy;
        }
        
        public void Tick()
        {
            _patrollingEnemy.Movement.Move(_patrollingEnemy.TargetData.Target);
            _patrollingEnemy.Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
        }

        public void OnEnter() { }
        public void OnExit() { }
    }
}