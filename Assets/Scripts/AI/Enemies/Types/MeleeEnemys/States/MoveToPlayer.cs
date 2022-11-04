using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Types.MeleeEnemys.States
{
    public sealed class MoveToPlayer : SM.IState
    {
        private readonly MeleeEnemy _meleeEnemy;
        private static readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public MoveToPlayer(MeleeEnemy meleeEnemy)
        {
            _meleeEnemy = meleeEnemy;
        }
        
        public void OnEnter() { }
        public void OnExit() { }

        public void Tick()
        {
            _meleeEnemy.Movement.Move(_meleeEnemy.TargetData.Target);
            _meleeEnemy.Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
        }
    }
}