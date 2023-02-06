using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Components
{
    public class Arrow : Projectile
    {
        public void Setup(Vector2 velocity, Vector3 direction)
        {
            MyRigidbody.velocity = velocity.normalized * Speed;
            transform.rotation = Quaternion.Euler(direction);
        }
    
        public override void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out Player _))
                Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player _))
                Destroy(gameObject);
        }
    }
}
