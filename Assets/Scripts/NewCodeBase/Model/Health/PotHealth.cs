using System;
using Remagures.Factories;
using Remagures.View.Pot;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Remagures.Model.Health
{
    public sealed class PotHealth : IHealth
    {
        public int MaxValue => _health.MaxValue;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;
        
        private readonly IHealth _health = new Health(1);
        
        private readonly IPotView _potView;
        private readonly IGameObjectFactory _lootFactory;
        private readonly Vector3 _lootSpawnPosition;

        public PotHealth(IPotView potView, IGameObjectFactory lootFactory, Vector3 lootSpawnPosition)
        {
            _potView = potView ?? throw new ArgumentNullException(nameof(potView));
            _lootFactory = lootFactory ?? throw new ArgumentNullException(nameof(lootFactory));
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(1);
            _potView.PlaySmashAnimation();
            _lootFactory.Create(_lootSpawnPosition);
        }

        public void Heal(int amount) { }
    }
}