using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    [Serializable]
    public sealed class SharedCollider2D : SharedVariable<Collider2D>
    {
        public static implicit operator SharedCollider2D(Collider2D value) 
            => new() { Value = value };
    }
}