using System;
using Remagures.Model.Character;

namespace Remagures.Model.AI.NPC.StateSetupers
{
    public class TalkingStateSetuper : IStateSetuper
    {
        private readonly DudeStates _states;
        private readonly INPCInteractable _npcInteractable;

        public TalkingStateSetuper(DudeStates states, INPCInteractable npcInteractable)
        {
            _states = states;
            _npcInteractable = npcInteractable ?? throw new ArgumentNullException(nameof(npcInteractable));
        }

        public void Setup(StateMachine.StateMachine stateMachine)
        {
            stateMachine.AddTransition(_states.WaitingState, _states.TalkingState, () => _npcInteractable.HasInteractionStarted);
            stateMachine.AddTransition(_states.TalkingState, _states.WaitingState, () => _npcInteractable.HasInteractionEnded);
        }
    }
}