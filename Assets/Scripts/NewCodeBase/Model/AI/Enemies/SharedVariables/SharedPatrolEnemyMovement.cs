using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    public sealed class SharedPatrolEnemyMovement : SharedVariable<PatrolEnemyMovement>
    {
        public static implicit operator SharedPatrolEnemyMovement(PatrolEnemyMovement value) 
            => new() { Value = value };
    }
}