using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockable : MonoBehaviour, IKnockable
{
    [SerializeField] private Enemy _enemy;
    [field: SerializeField] public LayerMask LayerMask { get; private set; }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        if (_enemy.CurrentState != EnemyState.Stagger)
        {
            _enemy.ChangeState(EnemyState.Stagger);
            StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            _enemy.ChangeState(EnemyState.None);
        }
    }
}
