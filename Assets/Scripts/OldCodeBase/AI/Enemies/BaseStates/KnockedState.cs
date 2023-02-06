using Remagures.AI.StateMachine;
using UnityEngine;

namespace Remagures.AI.Enemies
{
    public class KnockedState : IState
    {
        private readonly IEnemy _enemy;
        private readonly int KNOCKED_ANIMATOR_NAME = Animator.StringToHash("knocked");

        public KnockedState(IEnemy enemy)
            => _enemy = enemy;

        public void OnEnter()
        {
            _enemy.Movement.StopMoving();
            _enemy.Animations.Animator.logWarnings = false;
            
            if (_enemy.Animations.Animator.GetBool(KNOCKED_ANIMATOR_NAME))
                _enemy.Animations.Animator.SetBool(KNOCKED_ANIMATOR_NAME, true);
        }

        public void Tick() { }
        public void OnExit() { }
    }
}