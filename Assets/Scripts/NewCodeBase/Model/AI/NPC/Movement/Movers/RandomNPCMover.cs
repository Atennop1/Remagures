using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Remagures.Model.AI.NPC
{
    public class RandomNPCMover : IRandomNPCMover
    {
        public Vector2 MoveDirection { get; private set; }

        private readonly INPCMovement _npcMovement;
        private readonly Transform _npcTransform;

        public RandomNPCMover(INPCMovement npcMovement, Transform npcTransform)
        {
            _npcMovement = npcMovement ?? throw new ArgumentNullException(nameof(npcMovement));
            _npcTransform = npcTransform ?? throw new ArgumentNullException(nameof(npcTransform));
        }

        public void Move()
        {
            var targetPosition = (Vector2)_npcTransform.position + MoveDirection * 2;
            
            if (Physics2D.Raycast(_npcTransform.position, MoveDirection, 2f))
            {
                _npcMovement.Move(targetPosition);
            }
            else
            {
                ChooseDifferentDirection();
                Move();
            }
        }

        public void StopMoving()
            => _npcMovement.StopMoving();

        public void ChooseDifferentDirection()
        {
            var previousDirection = MoveDirection;
        
            while (previousDirection == MoveDirection)
                ChangeDirection();
        }

        private void ChangeDirection()
        {
            var direction = Random.Range(0, 4);
            
            MoveDirection = direction switch
            {
                0 => Vector2.right,
                1 => Vector2.left,
                2 => Vector2.up,
                3 => Vector2.down,
            };
        }
    }
}