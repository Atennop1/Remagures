using UnityEngine;

namespace Remagures.Model.Projectiles
{
    public sealed class DestroyOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D()
            => Destroy(gameObject);
    }
}