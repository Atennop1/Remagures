using System;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.Cutscenes
{
    public class MoveAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished => !_playerMovement.IsMoving;
        
        private readonly Vector2 _moveTo;
        private readonly PlayerMovement _playerMovement;

        public MoveAction(PlayerMovement playerMovement, Vector2 moveTo)
        {
            _moveTo = moveTo;
            _playerMovement = playerMovement ? playerMovement : throw new ArgumentNullException(nameof(playerMovement));
        }

        public void Start()
        {
            IsStarted = true;
            _playerMovement.MoveTo(_moveTo);
        }
        
        public void Finish() { }
    }
}