using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class StandingNode : Action
    {
        public SharedCharacterMovement SharedMovement;

        public override void OnAwake()
            => SharedMovement.Value.StopMoving();
    }
}