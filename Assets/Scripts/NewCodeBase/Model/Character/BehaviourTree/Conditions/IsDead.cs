using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class IsDead : Conditional
    {
        public SharedHealth SharedHealth;

        public override TaskStatus OnUpdate() 
            => SharedHealth.Value.IsDead ? TaskStatus.Success : TaskStatus.Failure;
    }
}