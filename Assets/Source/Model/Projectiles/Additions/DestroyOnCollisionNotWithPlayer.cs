using Remagures.Model.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Projectiles
{
    public sealed class DestroyOnCollisionNotWithPlayer : SerializedMonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out PhysicsCharacter _))
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out PhysicsCharacter _))
                Destroy(gameObject);
        }
    }
}