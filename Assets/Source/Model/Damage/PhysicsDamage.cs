using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Damage
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PhysicsDamage : SerializedMonoBehaviour
    {
        private IDamage _damage;

        public void Construct(IDamage damage)
            => _damage = damage ?? throw new ArgumentNullException(nameof(damage));

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out ITarget target))
                _damage.ApplyTo(target);
        }
    }
}