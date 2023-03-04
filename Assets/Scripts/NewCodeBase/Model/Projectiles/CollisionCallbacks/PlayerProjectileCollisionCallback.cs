using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.CollisionCallbacks
{
    public class PlayerProjectileCollisionCallback : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out PhysicsCharacter _))
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PhysicsCharacter _))
                Destroy(gameObject);
        }
    }
}