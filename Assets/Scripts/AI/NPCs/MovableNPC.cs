using Remagures.AI.NPCs.Components;
using UnityEngine;

namespace Remagures.AI.NPCs
{
    public abstract class MovableNPC : NPC
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}
