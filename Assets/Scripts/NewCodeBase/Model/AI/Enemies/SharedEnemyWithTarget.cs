using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedEnemyWithTarget : SharedVariable<IEnemyWithTarget>
    {
        public static implicit operator SharedEnemyWithTarget(EnemyWithTarget value) 
            => new() { Value = value };
    }
}