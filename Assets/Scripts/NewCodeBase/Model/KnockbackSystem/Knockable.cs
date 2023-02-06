using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.KnockbackSystem
{
    public sealed class Knockable : IKnockable
    {
        public LayerMask InteractionMask { get; }
        public bool IsKnocked { get; private set; }

        private readonly Rigidbody2D _rigidbody;
        
        public Knockable(Rigidbody2D rigidbody, LayerMask interactionMask)
        {
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            InteractionMask = interactionMask;
        }
        
        public async void Knock(int knockTimeInMilliseconds)
        {
            IsKnocked = true;
            await UniTask.Delay(knockTimeInMilliseconds);
            
            _rigidbody.velocity = Vector2.zero;
            IsKnocked = false;
        }
    }
}