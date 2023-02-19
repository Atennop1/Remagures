using Remagures.Model.AI.StateMachine;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class WaitingState : IState
    {
        private readonly NPCAnimations _npcAnimations;
        private readonly Transform _npcTransform;
        private readonly Transform _target;
        
        public void Update()
        {
            var direction = _target.position - _npcTransform.position;
            _npcAnimations.SetAnimationsVector(direction);
        }

        public void OnEnter()
            => _npcAnimations.ActivateIsStaying();

        public void OnExit() { }
    }
}