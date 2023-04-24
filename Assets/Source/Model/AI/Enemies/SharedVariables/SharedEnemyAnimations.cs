using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedEnemyAnimations : SharedVariable<IEnemyAnimations>
    {
        public static SharedEnemyAnimations FromIEnemyAnimations(IEnemyAnimations value) 
            => new() { Value = value };
    }
}