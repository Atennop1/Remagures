using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedEnemyAttacker : SharedVariable<EnemyAttacker>
    {
        public static implicit operator SharedEnemyAttacker(EnemyAttacker value) 
            => new() { Value = value };
    }
}