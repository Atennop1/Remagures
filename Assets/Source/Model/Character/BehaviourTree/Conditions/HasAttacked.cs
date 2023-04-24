using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class HasAttacked : Conditional
    {
        public SharedAttackInput AttackInput;

        public override TaskStatus OnUpdate() 
            => AttackInput.Value.HasAttacked ? TaskStatus.Success : TaskStatus.Failure;
    }
}