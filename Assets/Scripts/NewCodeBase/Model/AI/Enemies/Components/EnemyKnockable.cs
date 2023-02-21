using Remagures.Model.Knockback;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class EnemyKnockable : IKnockable
    {
        public bool IsKnocking => _knockable.IsKnocking;
        public LayerMask InteractionMask => _knockable.InteractionMask;
        
        private readonly IKnockable _knockable;
        private readonly StateMachine _enemyStateMachine;

        public void Knock(int knockTimeInMilliseconds)
        {
            if (!_enemyStateMachine.CanSetState(typeof(KnockedState)))
            {
                _knockable.StopKnocking();
                return;
            }

            _knockable.Knock(knockTimeInMilliseconds);
        }

        public void StopKnocking()
            => _knockable.StopKnocking();
    }
}
