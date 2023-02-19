using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class RandomBoundedNPCMover : IRandomNPCMover
    {
        public Vector2 MoveDirection => _randomNpcMover.MoveDirection;
        
        private readonly IRandomNPCMover _randomNpcMover;
        private readonly Transform _npcTransform;
        private readonly Collider2D _boundsCollider;
        
        public void Move()
        {
            var targetPosition = (Vector2)_npcTransform.position + _randomNpcMover.MoveDirection * 2;
            
            if (_boundsCollider.bounds.Contains(targetPosition))
            {
                _randomNpcMover.Move();
            }
            else
            {
                _randomNpcMover.ChooseDifferentDirection();
                Move();
            }
        }

        public void StopMove()
            => _randomNpcMover.StopMove();

        public void ChooseDifferentDirection()
            => _randomNpcMover.ChooseDifferentDirection();
    }
}