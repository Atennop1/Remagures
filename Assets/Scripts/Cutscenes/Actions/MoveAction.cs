﻿using Remagures.Player.Components;
using UnityEngine;

namespace Remagures.Cutscenes.Actions
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
            _playerMovement = playerMovement;
        }

        public void Start()
        {
            IsStarted = true;
            _playerMovement.Move(_moveTo);
        }
    }
}