using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.NPC
{
    [Serializable]
    public sealed class SharedINPCMovement : SharedVariable<INPCMovement>
    {
        public static SharedINPCMovement FromINPCMovement(INPCMovement value) 
            => new() { Value = value };
    }
}