using System;
using Remagures.AI.StateMachine;
using Remagures.View.Character;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class StandingState : IState
    {
        private readonly Rigidbody2D _characterRigidbody;
        private readonly ICharacterMovementView _movementView;

        public StandingState(Rigidbody2D characterRigidbody, ICharacterMovementView movementView)
        {
            _characterRigidbody = characterRigidbody ?? throw new ArgumentNullException(nameof(characterRigidbody));
            _movementView = movementView ?? throw new ArgumentNullException(nameof(movementView));
        }

        public void OnEnter()
        {
            _characterRigidbody.velocity = Vector2.zero;
            _movementView.EndMoveAnimation();
        }
        
        public void Update() { }
        public void OnExit() { }
    }
}