using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedPatrolEnemyMovement : SharedVariable<PatrolEnemyMovement>
    {
        public static implicit operator SharedPatrolEnemyMovement(PatrolEnemyMovement value) 
            => new() { Value = value };
    }
}