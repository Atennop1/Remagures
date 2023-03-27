using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.Conditions
{
    public sealed class IsPlayerInBounds : Conditional
    {
        public SharedEnemyWithTarget SharedEnemyWithTarget;
        public SharedCollider2D BoundsCollider2D;

        public override TaskStatus OnUpdate()
            => BoundsCollider2D.Value.bounds.Contains(SharedEnemyWithTarget.Value.TargetData.Transform.position)
               ? TaskStatus.Success
               : TaskStatus.Failure;
    }
}