using System;
using BehaviorDesigner.Runtime;
using Remagures.View.Interactable;

namespace Remagures.Model.AI.NPC
{
    [Serializable]
    public sealed class SharedINPCMovementView : SharedVariable<INPCMovementView>
    {
        public static SharedINPCMovementView FromINPCMovementView(INPCMovementView value) 
            => new() { Value = value };
    }
}