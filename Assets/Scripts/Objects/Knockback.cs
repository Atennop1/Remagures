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
        if (collision.TryGetComponent<IKnockable>(out IKnockable knockable))
        {
            Rigidbody2D hit = collision.GetComponentInParent<Rigidbody2D>();
            if (hit != null && knockable.LayerMask == (knockable.LayerMask | (1 << gameObject.layer)) && _lastCollider == null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * _strength;
                hit.DOMove(hit.transform.position + difference, _knockTime);

                knockable.Knock(hit, _knockTime);
                _lastCollider = collision;
                StartCoroutine(LastColReset());
            }
        }
    }
    
    private IEnumerator LastColReset()
    {
        yield return null;
        _lastCollider = null;
    }
}
