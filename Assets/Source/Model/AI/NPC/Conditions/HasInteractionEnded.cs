using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.NPC
{
    public sealed class HasInteractionEnded : Conditional
    {
        public SharedNPCInteractable SharedNpcInteractable;

        public override TaskStatus OnUpdate()
            => SharedNpcInteractable.Value.HasInteractionEnded ? TaskStatus.Success : TaskStatus.Failure;
    }
}