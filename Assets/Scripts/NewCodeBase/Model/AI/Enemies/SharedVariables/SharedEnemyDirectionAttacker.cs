using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedEnemyDirectionAttacker : SharedVariable<EnemyDirectionAttacker>
    {
        public static implicit operator SharedEnemyDirectionAttacker(EnemyDirectionAttacker value) 
            => new() { Value = value };
    }
}