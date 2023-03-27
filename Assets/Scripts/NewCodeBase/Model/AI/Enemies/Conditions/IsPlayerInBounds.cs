using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies
{
    public sealed class IsPlayerInBounds : Conditional
    {
        public SharedEnemyTargetData SharedEnemyTargetData;
        public SharedCollider2D BoundsCollider2D;

        public override TaskStatus OnUpdate()
            => BoundsCollider2D.Value.bounds.Contains(SharedEnemyTargetData.Value.Transform.position)
               ? TaskStatus.Success
               : TaskStatus.Failure;
    }
}