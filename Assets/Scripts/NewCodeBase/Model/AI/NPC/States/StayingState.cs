using Remagures.Model.AI.StateMachine;

namespace Remagures.Model.AI.NPC
{
    public sealed class StayingState : IState
    {
        private readonly NPCAnimations _npcAnimations;

        public void OnEnter()
            => _npcAnimations.ActivateIsStaying();

        public void Update() { }
        public void OnExit() { }
    }
}