using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.KnockbackSystem
{
    public class Knocker
    {
        private readonly float _strength;
        private readonly int _knockTimeInMilliseconds;

        public Knocker(float strength, int knockTimeInMilliseconds)
        {
            _strength = strength.ThrowExceptionIfLessOrEqualsZero();
            _knockTimeInMilliseconds = knockTimeInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Knock(Rigidbody2D rigidbody, IKnockable knockable, Vector2 direction)
        {
            if (rigidbody == null)
                throw new ArgumentNullException(nameof(rigidbody));

            if (knockable == null)
                throw new ArgumentNullException(nameof(knockable));

            rigidbody.AddForce(direction * _strength, ForceMode2D.Impulse);
            knockable.Knock(_knockTimeInMilliseconds);
        }
    }
}
