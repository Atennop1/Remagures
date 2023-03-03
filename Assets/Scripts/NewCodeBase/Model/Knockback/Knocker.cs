using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.Knockback
{
    public sealed class Knocker
    {
        private readonly float _strength;
        private readonly int _knockTimeInMilliseconds;

        public Knocker(float strength, int knockTimeInMilliseconds)
        {
            _strength = strength.ThrowExceptionIfLessOrEqualsZero();
            _knockTimeInMilliseconds = knockTimeInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Knock(IKnockable knockable, Vector2 direction)
        {
            if (knockable == null)
                throw new ArgumentNullException(nameof(knockable));
            
            knockable.Knock(direction * _strength, _knockTimeInMilliseconds);
        }
    }
}
