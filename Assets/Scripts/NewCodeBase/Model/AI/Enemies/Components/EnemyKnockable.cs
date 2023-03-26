using System;
using Remagures.Model.Knockback;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class EnemyKnockable : IKnockable
    {
        public bool IsKnocking => _knockable.IsKnocking;
        public LayerMask KnockingMask => _knockable.KnockingMask;
        
        private readonly IKnockable _knockable;
        private readonly StateMachine _enemyStateMachine;

        public EnemyKnockable(IKnockable knockable, StateMachine enemyStateMachine)
        {
            _knockable = knockable ?? throw new ArgumentNullException(nameof(knockable));
            _enemyStateMachine = enemyStateMachine ?? throw new ArgumentNullException(nameof(enemyStateMachine));
        }
        
        public void Knock(Vector2 forceVector, int timeInMilliseconds)
        {
            if (!_enemyStateMachine.CanSetState(typeof(KnockedState)))
            {
                _knockable.StopKnocking();
                return;
            }

            _knockable.Knock(forceVector, timeInMilliseconds);
        }

        public void StopKnocking()
            => _knockable.StopKnocking();
    }
}
