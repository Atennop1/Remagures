using System.Collections;
using Remagures.AI.StateMachine;
using Remagures.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.AI.Enemies
{
    public class EnemyKnockable : SerializedMonoBehaviour, IKnockable
    {
        [SerializeField] private IEnemy _enemy;
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }
        
        public bool IsKnocked { get; private set; }
        private IState _knockedState;

        public void Knock(Rigidbody2D myRigidbody, float knockTime)
        {
            if (_enemy.StateMachine.StateAlreadySet(_knockedState) || _enemy.StateMachine.StateBannedInCurrentContext(_knockedState))
                return;
            
            StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
        }

        private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
        {
            if (!myRigidbody) yield break;

            IsKnocked = true;
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            IsKnocked = false;
        }

        private void Start() 
            => _knockedState = new KnockedState(_enemy);
    }
}
