using System;
using Remagures.Factories;
using Remagures.View.Pot;

namespace Remagures.Model.Health
{
    public sealed class PotHealth : IHealth
    {
        public IReadOnlyMaxHealth Max => _health.Max;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;

        private readonly IHealth _health;
        private readonly IPotView _potView;
        private readonly IGameObjectFactory _lootFactory;

        public PotHealth(IHealth health, IPotView potView, IGameObjectFactory lootFactory)
        {
            _potView = potView ?? throw new ArgumentNullException(nameof(potView));
            _lootFactory = lootFactory ?? throw new ArgumentNullException(nameof(lootFactory));
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(1);
            _potView.PlaySmashAnimation();
            _lootFactory.Create();
        }

        public void Heal(int amount) { }
    }
}