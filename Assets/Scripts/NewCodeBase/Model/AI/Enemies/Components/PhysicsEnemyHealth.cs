using System;
using Remagures.Factories;
using Remagures.Model.Health;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class PhysicsEnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private IGameObjectFactory _deathEffectFactory;
        [SerializeField] private IGameObjectFactory _lootFactory;
        
        public IReadOnlyMaxHealth Max => _health.Max;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;
        
        private IHealth _health;

        public void Construct(IHealth health)
            => _health = health ?? throw new ArgumentNullException(nameof(health));

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            
            if (!IsDead)
                return;
        
            CreateDeathEffect();
            _lootFactory.Create(transform.position + new Vector3(0, 0.0000001f));
        }

        public void Heal(int amount)
            => _health.Heal(amount);

        private void CreateDeathEffect()
        {
            var effect = _deathEffectFactory.Create(transform.position);
            Destroy(effect, 1f);
        }
    }
}