using BehaviorDesigner.Runtime;
using Remagures.Model.Input;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedInteractingInput : SharedVariable<IInteractingInput>
    {
        public static SharedInteractingInput FromIInteractingInput(IInteractingInput value) 
            => new() { Value = value };
    }
}