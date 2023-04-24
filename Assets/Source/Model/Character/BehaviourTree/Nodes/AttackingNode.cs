using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class AttackingNode : Action
    {
        public SharedCharacterAttacker SharedCharacterAttacker;

        public override void OnAwake()
            => SharedCharacterAttacker.Value.UseAttack();
    }
}