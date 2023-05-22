using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Projectiles
{
    public sealed class DestroyAfterTime : SerializedMonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private float _lifeTimer;

        private void Awake()
            => _lifeTimer = _lifeTime;

        private void Update()
        {
            _lifeTimer -= Time.deltaTime;
            
            if (_lifeTimer <= 0)
                Destroy(gameObject);
        }
    }
}