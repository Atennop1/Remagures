using System;
using Remagures.Model.Character;

namespace Remagures.Model.AI.NPC.StateSetupers
{
    public class WaitingStateSetuper : IStateSetuper
    {
        private readonly DudeStates _states;
        private readonly NPCPlayerDetector _playerDetector;

        public WaitingStateSetuper(DudeStates states, NPCPlayerDetector playerDetector)
        {
            _states = states;
            _playerDetector = playerDetector ?? throw new ArgumentNullException(nameof(playerDetector));
        }

        public void Setup(StateMachine.StateMachine stateMachine)
        {
            stateMachine.AddTransition(_states.StayingState, _states.WaitingState, () => _playerDetector.HasPlayerDetected);
            stateMachine.AddTransition(_states.WalkingState, _states.WaitingState, () => _playerDetector.HasPlayerDetected);
            stateMachine.AddTransition(_states.WaitingState, _states.StayingState, () => _playerDetector.HasPlayerUndetected);
        }
    }
}