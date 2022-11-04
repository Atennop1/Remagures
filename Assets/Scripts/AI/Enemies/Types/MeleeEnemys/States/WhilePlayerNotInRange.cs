using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.MeleeEnemys.States
{
    public sealed class WhilePlayerNotInRange : SM.IState
    {
        private readonly MeleeEnemy _meleeEnemy;
        private static readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public WhilePlayerNotInRange(MeleeEnemy meleeEnemy)
        {
            _meleeEnemy = meleeEnemy;
        }
        
        public void OnEnter()
        {
            _meleeEnemy.Movement.StopMoving();
            _meleeEnemy.Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
        }

        public void OnExit() { }

        public void Tick() { }
    }
}
