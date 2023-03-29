using BehaviorDesigner.Runtime;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedCharacterMovement : SharedVariable<ICharacterMovement>
    {
        public static SharedCharacterMovement FromICharacterMovement(ICharacterMovement value) 
            => new() { Value = value };
    }
}