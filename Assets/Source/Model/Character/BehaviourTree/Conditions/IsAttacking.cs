using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class IsAttacking : Conditional
    {
        public SharedCharacterAttacker SharedCharacterAttacker;

        public override TaskStatus OnUpdate() 
            => SharedCharacterAttacker.Value.IsAttacking ? TaskStatus.Success : TaskStatus.Failure;
    }
}