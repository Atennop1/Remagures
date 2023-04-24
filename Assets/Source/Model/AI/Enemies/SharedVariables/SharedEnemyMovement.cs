using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedEnemyMovement : SharedVariable<IEnemyMovement>
    {
        public static SharedEnemyMovement FromIEnemyMovement(IEnemyMovement value) 
            => new() { Value = value };
    }
}