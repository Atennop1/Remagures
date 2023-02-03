using UnityEngine;

namespace Remagures.Components
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float MagicCost { get; private set; }

        protected Rigidbody2D MyRigidbody { get; private set; }
        private float _lifeTimeSeconds;

        private void Awake()
        {
            MyRigidbody = GetComponent<Rigidbody2D>();
            _lifeTimeSeconds = LifeTime;
        }

        private void Update()
        {
            _lifeTimeSeconds -= UnityEngine.Time.deltaTime;
            if (_lifeTimeSeconds <= 0)
                Destroy(gameObject);
        }

        public void Launch(Vector2 initialVelocity)
        {
            MyRigidbody.velocity = initialVelocity * Speed;
        }

        public virtual void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
