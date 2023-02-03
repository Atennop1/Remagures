using UnityEngine;

namespace Remagures.AI
{
    public abstract class MovableNPC : NPC
    {
        [field: SerializeField, Space] public float Speed { get; private set; }
    }
}
