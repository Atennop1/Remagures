using Remagures.Model.AI;
using Remagures.Model.Input;
using UnityEngine;

namespace Remagures.Model.Character
{
    public class StandingStateSetuper : IStateSetuper
    {
        private readonly CharacterStates _states;
        private readonly IMovementInput _movementInput;
        
        public void Setup(StateMachine stateMachine)
        {
            stateMachine.SetState(_states.StandingState);
            stateMachine.AddTransition(_states.StandingState, _states.WalkingState, () => _movementInput.MoveDirection != Vector2.zero);
            stateMachine.AddTransition(_states.WalkingState, _states.StandingState, () => _movementInput.MoveDirection == Vector2.zero);
        }
    }
}