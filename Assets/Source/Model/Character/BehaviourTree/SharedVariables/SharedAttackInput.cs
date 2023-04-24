using System;
using BehaviorDesigner.Runtime;
using Remagures.Model.Input;

namespace Remagures.Model.Character.BehaviourTree
{
    [Serializable]
    public sealed class SharedAttackInput : SharedVariable<IAttackInput>
    {
        public static SharedAttackInput FromIAttackInput(IAttackInput value) 
            => new() { Value = value };
    }
}