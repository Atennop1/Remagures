using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public sealed class SetRandomMovementDirection : Action
    {
        public SharedVector3 SharedMoveDirection;
        
        public override TaskStatus OnUpdate()
        {
            var previousDirection = SharedMoveDirection.Value;
        
            while (previousDirection == SharedMoveDirection.Value)
                ChangeDirection();

            return TaskStatus.Success;
        }

        private void ChangeDirection()
        {
            var direction = Random.Range(0, 4);
            
            SharedMoveDirection.Value = direction switch
            {
                0 => Vector3.right,
                1 => Vector3.left,
                2 => Vector3.up,
                3 => Vector3.down,
            };
        }
    }
}