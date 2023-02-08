using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.Knockback;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class CharacterKnockable : IKnockable
    {
        public LayerMask InteractionMask => _knockable.InteractionMask;
        public bool IsKnocked => _knockable.IsKnocked;

        private readonly IKnockable _knockable;
        private readonly Player _player;

        public CharacterKnockable(Player player, IKnockable knockable)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _knockable = knockable ?? throw new ArgumentNullException(nameof(knockable));
        }

        public async void Knock(int knockTimeInMilliseconds)
        {
            if (_player.CurrentState == PlayerState.Stagger) 
                return;
        
            _player.ChangeState(PlayerState.Stagger);
            _knockable.Knock(knockTimeInMilliseconds);

            await UniTask.Delay(knockTimeInMilliseconds);
            _player.ChangeState(PlayerState.Idle);
        }
    }
}