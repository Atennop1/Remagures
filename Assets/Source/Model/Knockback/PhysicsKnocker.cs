using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Knockback
{
    public sealed class PhysicsKnocker : SerializedMonoBehaviour
    {
        private IKnocker _knocker;

        public void Construct(IKnocker knocker)
            => _knocker = knocker ?? throw new ArgumentNullException(nameof(knocker));

        private void OnTriggerEnter2D(Collider2D collisionObject)
        {
            if (collisionObject.TryGetComponent(out IKnockable knockable) && (knockable.KnockingMask.value & (1 << collisionObject.gameObject.layer)) > 0)
                _knocker.Knock(knockable, collisionObject.transform.position - transform.position);
        }
    }
}