using Sirenix.OdinInspector;

namespace Remagures.Model.Projectiles
{
    public sealed class DestroyOnCollision : SerializedMonoBehaviour
    {
        private void OnCollisionEnter2D()
            => Destroy(gameObject);
    }
}