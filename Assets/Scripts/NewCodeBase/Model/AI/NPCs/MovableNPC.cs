using UnityEngine;

namespace Remagures.Model.AI.NPCs
{
    public abstract class MovableNPC : NPC
    {
        [field: SerializeField, Space] public float Speed { get; private set; }
    }
}
