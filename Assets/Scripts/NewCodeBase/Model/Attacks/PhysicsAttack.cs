using System;
using UnityEngine;

namespace Remagures.Model.Attacks
{
    public sealed class PhysicsAttack : MonoBehaviour
    {
        private IAttack _attack;

        public void Construct(IAttack attack)
            => _attack = attack ?? throw new ArgumentNullException(nameof(attack));

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out ITarget target))
                _attack.ApplyTo(target);
        }
    }
}