using System;
using Remagures.AI.StateMachine;
using Remagures.Model.Input;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class WalkingState : IState
    {
        private readonly ICharacterMovement _characterMovement;

        public WalkingState(ICharacterMovement characterMovement)
            => _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));

        public void Update()
        {
            var endPosition = _characterMovement.Transform.position + (Vector3)_characterMovement.CharacterLookDirection * 2;
            _characterMovement.MoveTo(endPosition);
        }

        public void OnEnter() { }
        public void OnExit() { }
    }
}