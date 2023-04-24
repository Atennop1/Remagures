using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.NPC
{
    [Serializable]
    public sealed class SharedRandomMovementData : SharedVariable<RandomMovementData>
    {
        public static implicit operator SharedRandomMovementData(RandomMovementData value) 
            => new() { Value = value };
    }
}