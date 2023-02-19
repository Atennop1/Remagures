using System.Collections;
using Remagures.Model.AI.StateMachine;
using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class EnemyKnockable : SerializedMonoBehaviour, IKnockable
    {
        [SerializeField] private IEnemy _enemy;
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }
        
        public bool IsKnocking { get; private set; }
        private IState _knockedState;

        public void Knock(Rigidbody2D myRigidbody, float knockTime)
        {
            if (_enemy.StateMachine.IsStateAlreadySet(_knockedState) || _enemy.StateMachine.IsStateBannedInCurrentContext(_knockedState))
                return;
            
            StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
        }

        private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
        {
            if (!myRigidbody) yield break;

            IsKnocking = true;
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            IsKnocking = false;
        }

        private void Start() 
            => _knockedState = new KnockedState(_enemy);
    }
}
