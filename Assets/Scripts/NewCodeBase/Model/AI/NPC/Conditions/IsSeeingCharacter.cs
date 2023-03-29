using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public sealed class IsSeeingCharacter : Conditional
    {
        public SharedInt ChaseRadius;
        public SharedTransform SharedNPCTransform;
        public SharedTransform SharedPlayerTransform;

        public override TaskStatus OnUpdate()
        {
            return Vector3.Distance(SharedPlayerTransform.Value.position, SharedNPCTransform.Value.position) <= ChaseRadius.Value
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}