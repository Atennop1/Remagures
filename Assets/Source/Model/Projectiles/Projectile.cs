using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.Projectiles
{
    public sealed class Projectile : IProjectile
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;
        
        public Projectile(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            _speed = speed.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Launch(Vector2 direction)
            => _rigidbody.velocity = direction * _speed;
    }
}
