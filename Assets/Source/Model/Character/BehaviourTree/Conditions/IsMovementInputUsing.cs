using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class IsMovementInputUsing : Conditional
    {
        public SharedMovementInput SharedMovementInput;

        public override TaskStatus OnUpdate() 
            => SharedMovementInput.Value.MoveDirection != Vector2.zero ? TaskStatus.Success : TaskStatus.Failure;
    }
}