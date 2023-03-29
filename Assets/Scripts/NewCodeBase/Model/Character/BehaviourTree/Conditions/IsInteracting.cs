using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class IsInteracting : Action
    {
        public SharedCharacterInteractor SharedCharacterInteractor;

        public override TaskStatus OnUpdate() 
            => SharedCharacterInteractor.Value.CurrentState == InteractionState.Active
                ? TaskStatus.Success
                : TaskStatus.Failure;
    }
}