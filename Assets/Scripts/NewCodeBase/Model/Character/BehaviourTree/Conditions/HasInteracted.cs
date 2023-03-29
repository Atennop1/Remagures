using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class HasInteracted : Conditional
    {
        public SharedInteractingInput SharedInteractingInput;

        public override TaskStatus OnUpdate() 
            => SharedInteractingInput.Value.HasInteracted ? TaskStatus.Success : TaskStatus.Failure;
    }
}