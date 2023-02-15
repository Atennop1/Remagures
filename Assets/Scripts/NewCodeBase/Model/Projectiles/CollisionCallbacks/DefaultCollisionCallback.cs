using UnityEngine;

namespace Remagures.Model.CollisionCallbacks
{
    public class DefaultCollisionCallback : MonoBehaviour
    {
        private void OnCollisionEnter2D()
            => Destroy(gameObject);
    }
}