using System;
using Remagures.View.Interactable;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public sealed class WaitingState : IState
    {
        private readonly INPCMovementView _npcMovementView;
        private readonly Transform _npcTransform;
        private readonly Transform _playerTransform;

        public WaitingState(INPCMovementView npcMovementView, Transform npcTransform, Transform playerTransform)
        {
            _npcMovementView = npcMovementView ?? throw new ArgumentNullException(nameof(npcMovementView));
            _npcTransform = npcTransform ?? throw new ArgumentNullException(nameof(npcTransform));
            _playerTransform = playerTransform ?? throw new ArgumentNullException(nameof(playerTransform));
        }

        public void Update()
        {
            var direction = _playerTransform.position - _npcTransform.position;
            _npcMovementView.DisplayMovementDirection(direction);
        }

        public void OnEnter()
            => _npcMovementView.DisplayStaying();

        public void OnExit() { }
    }
}