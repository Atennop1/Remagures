using UnityEngine;

public abstract class MovableNPC : NPC
{
    [field: SerializeField, Header("Speed")] public float Speed { get; private set; }
    
    protected abstract void Move();
}
