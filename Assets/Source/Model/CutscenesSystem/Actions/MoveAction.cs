using System;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class MoveAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished => !_characterMovement.IsMoving;
        
        private readonly Vector2 _moveTo;
        private readonly CharacterMovement _characterMovement;

        public MoveAction(CharacterMovement characterMovement, Vector2 moveTo)
        {
            _moveTo = moveTo;
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
        }

        public void Start()
        {
            IsStarted = true;
            _characterMovement.MoveTo(_moveTo);
        }
        
        public void Finish() { }
    }
}