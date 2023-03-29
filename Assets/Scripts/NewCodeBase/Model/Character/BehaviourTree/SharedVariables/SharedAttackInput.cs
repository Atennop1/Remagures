using BehaviorDesigner.Runtime;
using Remagures.Model.Input;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedAttackInput : SharedVariable<IAttackInput>
    {
        public static SharedAttackInput FromIAttackInput(IAttackInput value) 
            => new() { Value = value };
    }
}