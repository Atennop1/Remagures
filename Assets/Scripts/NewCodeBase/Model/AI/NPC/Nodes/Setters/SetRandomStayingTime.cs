using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public sealed class SetRandomStayingTime : Action
    {
        public SharedFloat SharedStayingTime;
        public SharedRandomMovementData SharedRandomMovementData;
        
        public override TaskStatus OnUpdate()
        {
            SharedStayingTime.Value = Random.Range(SharedRandomMovementData.Value.MinStayingTime, SharedRandomMovementData.Value.MaxStayingTime);
            return TaskStatus.Success;
        }
    }
}