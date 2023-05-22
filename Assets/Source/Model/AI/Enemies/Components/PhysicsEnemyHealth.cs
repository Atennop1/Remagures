using System;
using Remagures.Factories;
using Remagures.Model.Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class PhysicsEnemyHealth : SerializedMonoBehaviour, IHealth
    {
        [SerializeField] private IGameObjectFactory _deathEffectFactory;
        [SerializeField] private ILootFactory _lootFactory;
        
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
            _lootFactory.CreateRandom(transform.position + new Vector3(0, 0.0000001f));
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