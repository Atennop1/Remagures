using System.Collections;
using Remagures.AI.Enemies.Abstraction;
using Remagures.AI.Enemies.BaseStates;
using Remagures.Components.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Components
{
    public class EnemyKnockable : SerializedMonoBehaviour, IKnockable
    {
        [SerializeField] private IEnemy _enemy;
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }
        
        public bool IsKnocked { get; private set; }
        private SM.IState _knockedState;

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

        private void Start() => _knockedState = new KnockedState(_enemy);
    }
}
