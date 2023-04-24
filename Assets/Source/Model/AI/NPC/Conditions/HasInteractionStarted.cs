using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.NPC
{
    public sealed class HasInteractionStarted : Conditional
    {
        public SharedNPCInteractable SharedNpcInteractable;

        public override TaskStatus OnUpdate()
            => SharedNpcInteractable.Value.HasInteractionStarted ? TaskStatus.Success : TaskStatus.Failure;
    }
}