using UnityEngine;

namespace Remagures.Model
{
    public class PhysicsProjectile : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private float _lifeTimer;

        private void Awake()
            => _lifeTimer = _lifeTime;

        private void Update()
        {
            _lifeTimer -= UnityEngine.Time.deltaTime;
            
            if (_lifeTimer <= 0)
                Destroy(gameObject);
        }
    }
}