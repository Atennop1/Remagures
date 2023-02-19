using System;
using Remagures.Model.AI.StateMachine;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class InteractingState : IState
    {
        private readonly ICharacterInteractor _characterInteractor;
        private readonly Rigidbody2D _characterRigidbody;

        public InteractingState(ICharacterInteractor characterInteractor)
            => _characterInteractor = characterInteractor ?? throw new ArgumentNullException(nameof(characterInteractor));

        public void OnEnter()
        {
            _characterRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            _characterInteractor.Interact();
        }
        
        public void OnExit()
            => _characterRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        public void Update() { }
    }
}