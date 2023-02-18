using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.Knockback;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class CharacterKnockable : IKnockable
    {
        public LayerMask InteractionMask => _knockable.InteractionMask;
        public bool IsKnocking { get; private set; }

        private readonly IKnockable _knockable;

        public CharacterKnockable(IKnockable knockable)
        {
            _knockable = knockable ?? throw new ArgumentNullException(nameof(knockable));
        }

        public async void Knock(int knockTimeInMilliseconds)
        {
            IsKnocking = true;
            await UniTask.Delay(knockTimeInMilliseconds);
            IsKnocking = false;
        }
    }
}