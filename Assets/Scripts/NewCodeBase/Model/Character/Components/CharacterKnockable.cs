using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.AI;
using Remagures.Model.Knockback;
using UnityEngine;
using Random = System.Random;

namespace Remagures.Model.Character
{
    public sealed class CharacterKnockable : IKnockable
    {
        public LayerMask KnockingMask => _knockable.KnockingMask;
        public bool IsKnocking => _knockable.IsKnocking;

        private readonly IKnockable _knockable;
        private readonly StateMachine _characterStateMachine;

        public CharacterKnockable(IKnockable knockable, StateMachine characterStateMachine)
        {
            _knockable = knockable ?? throw new ArgumentNullException(nameof(knockable));
            _characterStateMachine = characterStateMachine ?? throw new ArgumentNullException(nameof(characterStateMachine));
        }
        
        public void Knock(Vector2 forceVector, int timeInMilliseconds)
        {
            if (!_characterStateMachine.CanSetState(typeof(KnockbackedState)))
                _knockable.StopKnocking();

            _knockable.Knock(forceVector, timeInMilliseconds);
        }

        public void StopKnocking()
            => _knockable.StopKnocking();
    }
}