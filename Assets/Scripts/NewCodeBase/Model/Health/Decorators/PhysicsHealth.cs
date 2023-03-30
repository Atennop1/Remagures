using System;
using UnityEngine;

namespace Remagures.Model.Health
{
    public sealed class PhysicsHealth : MonoBehaviour, IHealth
    {
        public IReadOnlyMaxHealth Max => _health.Max;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;
        
        private IHealth _health;

        public void Construct(IHealth health)
            => _health = health ?? throw new ArgumentNullException(nameof(health));

        public void TakeDamage(int amount) 
            => _health.TakeDamage(amount);

        public void Heal(int amount) 
            => _health.Heal(amount);
    }
}