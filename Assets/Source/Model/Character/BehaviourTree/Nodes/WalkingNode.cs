using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class WalkingNode : Action
    {
        public SharedCharacterMovement SharedMovement;

        public override TaskStatus OnUpdate()
        {
            var endPosition = SharedMovement.Value.Transform.position + (Vector3)SharedMovement.Value.CharacterLookDirection * 2;
            SharedMovement.Value.MoveTo(endPosition);
            return TaskStatus.Success;
        }
    }
}