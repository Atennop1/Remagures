using System;
using Remagures.Tools;
using Remagures.View.Interactable;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class NPCMovement : INPCMovement
    {
        private readonly INPCMovementView _npcMovementView;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;

        public NPCMovement(INPCMovementView npcMovementView, Rigidbody2D rigidbody, float speed)
        {
            _npcMovementView = npcMovementView ?? throw new ArgumentNullException(nameof(npcMovementView));
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            _speed = speed.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Move(Vector2 targetPosition)
        {
            var direction = targetPosition - _rigidbody.position;
            _rigidbody.MovePosition(_rigidbody.position + _speed * UnityEngine.Time.deltaTime * direction);
            
            _npcMovementView.DisplayMovement();
            _npcMovementView.DisplayMovementDirection(direction);
        }

        public void StopMoving()
        {
            _npcMovementView.DisplayStaying();
            _rigidbody.velocity = Vector2.zero;
        }
    }
}