using System;
using UnityEngine;

namespace Remagures.Model.Knockback
{
    public sealed class PhysicsKnockable : MonoBehaviour, IKnockable
    {
        private IKnockable _knockable;

        public LayerMask KnockingMask => _knockable.KnockingMask;
        public bool IsKnocking => _knockable.IsKnocking;
        
        public void Construct(IKnockable knockable)
            => _knockable = knockable ?? throw new ArgumentNullException(nameof(knockable));

        public void Knock(Vector2 forceVector, int timeInMilliseconds)
            => _knockable.Knock(forceVector, timeInMilliseconds);

        public void StopKnocking()
            => _knockable.StopKnocking();
    }
}