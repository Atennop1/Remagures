using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.NPC
{
    [Serializable]
    public sealed class SharedNPCInteractable : SharedVariable<INPCInteractable>
    {
        public static SharedNPCInteractable FromINPCInteractable(INPCInteractable value) 
            => new() { Value = value };
    }
}