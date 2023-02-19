using Remagures.View.Interactable;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class NPCMovement : INPCMovement
    {
        private readonly INPCMovementView _npcMovementView;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;

        public void Move(Vector2 targetPosition)
        {
            var direction = targetPosition - _rigidbody.position;
            _rigidbody.MovePosition(_rigidbody.position + _speed * UnityEngine.Time.deltaTime * direction);
            _npcMovementView.DisplayMovementDirection(direction);
        }

        public void StopMoving()
            => _rigidbody.velocity = Vector2.zero;
    }
}