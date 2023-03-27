using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedEnemyTargetData : SharedVariable<IEnemyTargetData>
    {
        public static SharedEnemyTargetData FromIEnemyTargetData(IEnemyTargetData value) 
            => new() { Value = value };
    }
}