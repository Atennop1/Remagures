using System;
using Remagures.Factories;
using Remagures.Model.Attacks;

namespace Remagures.Model.Character
{
    public sealed class CharacterAttack : IAttack
    {
        public int Damage => _attack.Damage;
        
        private readonly IHealthEffectFactory _healthEffectFactory;
        private readonly IAttack _attack;

        public CharacterAttack(IHealthEffectFactory healthEffectFactory, IAttack attack)
        {
            _healthEffectFactory = healthEffectFactory ?? throw new ArgumentNullException(nameof(healthEffectFactory));
            _attack = attack ?? throw new ArgumentNullException(nameof(attack));
        }

        public void ApplyTo(Target target)
        {
            target.Health.TakeDamage(Damage);
            _healthEffectFactory.Create(target, Damage).Activate();
        }
    }
}
