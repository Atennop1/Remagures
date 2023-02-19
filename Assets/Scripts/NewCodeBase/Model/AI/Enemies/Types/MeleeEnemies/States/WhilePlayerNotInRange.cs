using Remagures.Model.AI.StateMachine;
using UnityEngine;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class WhilePlayerNotInRange : IState
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

        public void Update() { }
    }
}
