using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Remagures.Model.AI.Enemies;

namespace Remagures.Model.AI.NPC
{
    public sealed class IsMovingDirectionInBounds : Conditional
    {
        public SharedTransform SharedNPCTransform;
        public SharedCollider2D SharedBoundsCollider;
        
        public SharedVector3 SharedMoveDirection;
        public SharedFloat SharedMovingTime;
        
        public override TaskStatus OnUpdate()
        {
            return SharedBoundsCollider.Value.bounds.Contains(SharedNPCTransform.Value.position + SharedMoveDirection.Value * SharedMovingTime.Value / 2)
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}