using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedRigidbody2D : SharedVariable<Rigidbody2D>
    {
        public static implicit operator SharedRigidbody2D(Rigidbody2D value) 
            => new() { Value = value };
    }
}