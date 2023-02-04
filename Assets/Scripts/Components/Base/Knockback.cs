using UnityEngine;

namespace Remagures.Components
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField] private float _strength;
        [SerializeField] private float _knockTime;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out IKnockable knockable) &&
                (collision.transform.parent == null || 
                !collision.transform.parent.TryGetComponent(out knockable))) return;
        
            var collisionRigidbody = collision.GetComponentInParent<Rigidbody2D>();
            if (collisionRigidbody == null || knockable.InteractionMask != (knockable.InteractionMask | (1 << gameObject.layer))) 
                return;
        
            var difference = collisionRigidbody.transform.position - transform.position;
            difference = difference.normalized * _strength;
        
            collisionRigidbody.AddForce(difference, ForceMode2D.Impulse);
            knockable.Knock(collisionRigidbody, _knockTime);
        }
    }
}
