using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.NPC
{
    public sealed class IsMoving : Conditional
    {
        public SharedBool SharedIsMoving;

        public override TaskStatus OnUpdate()
            => SharedIsMoving.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}