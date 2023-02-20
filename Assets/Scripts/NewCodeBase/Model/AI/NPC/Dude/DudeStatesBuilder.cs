using Remagures.View.Interactable;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class DudeStatesBuilder
    {
        private readonly NPCMovementStatesChanger _movementStatesChanger;
        private readonly IRandomNPCMover _randomNpcMover;
        private readonly RandomMovementData _randomMovementData;
        private readonly INPCMovementView _npcMovementView;
        private readonly Transform _npcTransform;
        private readonly Transform _playerTransform;
        
        public DudeStates Build()
        {
            var stayingState = new StayingState(_movementStatesChanger, _randomNpcMover, _randomMovementData);
            var talkingState = new TalkingState();
            var waitingState = new WaitingState(_npcMovementView, _npcTransform, _playerTransform);
            var walkingState = new WalkingState(_movementStatesChanger, _randomNpcMover, _randomMovementData);
            
            var states = new DudeStates(stayingState, talkingState, waitingState, walkingState);
            return states;
        }
    }
}