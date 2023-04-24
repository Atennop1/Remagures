using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class IsBotSeesPlayer : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyTargetData SharedEnemyTargetData;

        public override TaskStatus OnUpdate()
        {
            return Vector3.Distance(SharedEnemyTargetData.Value.Transform.position, 
                SharedEnemyMovement.Value.Transform.position) <= SharedEnemyTargetData.Value.ChaseRadius
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}