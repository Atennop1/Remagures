using BehaviorDesigner.Runtime;

namespace Remagures.Model.AI.NPC
{
    public sealed class SharedNPCInteractable : SharedVariable<INPCInteractable>
    {
        public static SharedNPCInteractable FromINPCInteractable(INPCInteractable value) 
            => new() { Value = value };
    }
}