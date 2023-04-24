using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public sealed class SetRandomMovingTime : Action
    {
        public SharedFloat SharedMovingTime;
        public SharedRandomMovementData SharedRandomMovementData;
        
        public override TaskStatus OnUpdate()
        {
            SharedMovingTime.Value = Random.Range(SharedRandomMovementData.Value.MinMovingTime, SharedRandomMovementData.Value.MaxMovingTime);
            return TaskStatus.Success;
        }
    }
}