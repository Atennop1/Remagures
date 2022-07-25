using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float _strength;
    [SerializeField] private float _knockTime;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IKnockable>(out IKnockable knockable) || 
        (collision.transform.parent != null && collision.transform.parent.TryGetComponent<IKnockable>(out knockable)))
        {
            Rigidbody2D hit = collision.GetComponentInParent<Rigidbody2D>();
            if (hit != null && knockable.LayerMask == (knockable.LayerMask | (1 << gameObject.layer)))
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * _strength;
                hit.DOMove(hit.transform.position + difference, _knockTime);
                knockable.Knock(hit, _knockTime);
            }
        }
    }
}
