using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.NPC
{
    public sealed class WaitingNode : Action
    {
        public SharedINPCMovement SharedNPCMovement;
        public SharedINPCMovementView SharedNPCMovementView;
        
        public SharedTransform SharedNPCTransform;
        public SharedTransform SharedPlayerTransform;
        
        public override TaskStatus OnUpdate()
        {
            var direction = SharedPlayerTransform.Value.position - SharedNPCTransform.Value.position;
            SharedNPCMovementView.Value.DisplayMovementDirection(direction);
            return TaskStatus.Success;
        }

        public override void OnAwake()
            => SharedNPCMovement.Value.StopMoving();
    }
}