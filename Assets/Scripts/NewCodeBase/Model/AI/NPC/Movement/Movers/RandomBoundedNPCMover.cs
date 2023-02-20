using System;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class RandomBoundedNPCMover : IRandomNPCMover
    {
        public Vector2 MoveDirection => _randomNpcMover.MoveDirection;
        
        private readonly IRandomNPCMover _randomNpcMover;
        private readonly Transform _npcTransform;
        private readonly Collider2D _boundsCollider;

        public RandomBoundedNPCMover(IRandomNPCMover randomNpcMover, Transform npcTransform, Collider2D boundsCollider)
        {
            _randomNpcMover = randomNpcMover ?? throw new ArgumentNullException(nameof(randomNpcMover));
            _npcTransform = npcTransform ?? throw new ArgumentNullException(nameof(npcTransform));
            _boundsCollider = boundsCollider ?? throw new ArgumentNullException(nameof(boundsCollider));
        }

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

        public void StopMoving()
            => _randomNpcMover.StopMoving();

        public void ChooseDifferentDirection()
            => _randomNpcMover.ChooseDifferentDirection();
    }
}