using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.Knockback
{
    public sealed class Knockable : IKnockable
    {
        public LayerMask InteractionMask { get; }
        public bool IsKnocking { get; private set; }

        private readonly Rigidbody2D _rigidbody;
        
        public Knockable(Rigidbody2D rigidbody, LayerMask interactionMask)
        {
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            InteractionMask = interactionMask;
        }
        
        public async void Knock(int knockTimeInMilliseconds)
        {
            IsKnocking = true;
            await UniTask.Delay(knockTimeInMilliseconds);
            
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