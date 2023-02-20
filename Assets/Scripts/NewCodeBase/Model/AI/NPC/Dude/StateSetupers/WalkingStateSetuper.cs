using System;
using Remagures.Model.Character;

namespace Remagures.Model.AI.NPC.StateSetupers
{
    public class WalkingStateSetuper : IStateSetuper
    {
        private readonly DudeStates _dudeStates;
        private readonly NPCMovementStatesChanger _movementStatesChanger;

        public WalkingStateSetuper(DudeStates dudeStates, NPCMovementStatesChanger movementStatesChanger)
        {
            _dudeStates = dudeStates;
            _movementStatesChanger = movementStatesChanger ?? throw new ArgumentNullException(nameof(movementStatesChanger));
        }

        public void Setup(StateMachine stateMachine)
        {
            stateMachine.AddTransition(_dudeStates.WalkingState, _dudeStates.StayingState, () => _movementStatesChanger.HasEndedMoving);
            stateMachine.AddTransition(_dudeStates.StayingState, _dudeStates.WalkingState, () => _movementStatesChanger.HasStartedMoving);
        }
    }
}