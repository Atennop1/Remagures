using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private string _otherTag;
    [SerializeField] private float _strength;
    [SerializeField] private float _knockTime;
    private Collider2D _lastCollider;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Breakable") && gameObject.CompareTag("PlayerAttack"))
            collision.GetComponent<Destroyable>().Smash();
            
        if (collision.gameObject.CompareTag(_otherTag))
        {
            Rigidbody2D hit = collision.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * _strength;
                hit.DOMove(hit.transform.position + difference, _knockTime);
                
                if (collision.gameObject.CompareTag("Enemy") && _lastCollider == null && collision.TryGetComponent<Enemy>(out Enemy enemy) && enemy.CurrentState != EnemyState.Stagger)
                {
                    _lastCollider = collision;
                    enemy.ChangeState(EnemyState.Stagger);
                    StartCoroutine(LastColReset());
                    enemy.Knock(hit, _knockTime);
                }
                if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy && collision.TryGetComponent<PlayerController>(out PlayerController player) && player.CurrentState != PlayerState.Stagger)
                {
                    player.CurrentState = PlayerState.Stagger;
                    player.GetComponent<PlayerKnockable>().Knock(hit, _knockTime);
                }
            }
        }
    }
    
    private IEnumerator LastColReset()
    {
        yield return null;
        _lastCollider = null;
    }
}
