using System;
using Remagures.Factories;
using Remagures.Model.Damage;

namespace Remagures.Model.Character
{
    public sealed class CharacterDamage : IDamage
    {
        public IDamageValue Value => _damage.Value;
        
        private readonly IHealthEffectFactory _healthEffectFactory;
        private readonly IDamage _damage;

        public CharacterDamage(IHealthEffectFactory healthEffectFactory, IDamage damage)
        {
            _healthEffectFactory = healthEffectFactory ?? throw new ArgumentNullException(nameof(healthEffectFactory));
            _damage = damage ?? throw new ArgumentNullException(nameof(damage));
        }

        public void ApplyTo(ITarget target)
        {
            target.Health.TakeDamage(Value.Get());
            _healthEffectFactory.Create(target, Value.Get()).Activate();
        }
    }
}
