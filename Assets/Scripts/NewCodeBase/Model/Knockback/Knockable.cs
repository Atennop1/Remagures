using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.Knockback
{
    public sealed class Knockable : IKnockable
    {
        public LayerMask KnockingMask { get; }
        public bool IsKnocking { get; private set; }

        private readonly Rigidbody2D _rigidbody;
        
        public Knockable(Rigidbody2D rigidbody, LayerMask interactionMask)
        {
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            KnockingMask = interactionMask;
        }
        
        public async void Knock(Vector2 forceVector, int timeInMilliseconds)
        {
            IsKnocking = true;
            _rigidbody.AddForce(forceVector, ForceMode2D.Impulse);
            
            await UniTask.Delay(timeInMilliseconds);
            
            if (!IsKnocking)
                return;

            StopKnocking();
        }

        public void StopKnocking()
        {
            _rigidbody.velocity = Vector2.zero;
            IsKnocking = false;
        }
    }
}