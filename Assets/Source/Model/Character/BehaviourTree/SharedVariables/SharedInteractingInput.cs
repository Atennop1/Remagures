using System;
using BehaviorDesigner.Runtime;
using Remagures.Model.Input;

namespace Remagures.Model.Character.BehaviourTree
{
    [Serializable]
    public sealed class SharedInteractingInput : SharedVariable<IInteractingInput>
    {
        public static SharedInteractingInput FromIInteractingInput(IInteractingInput value) 
            => new() { Value = value };
    }
}