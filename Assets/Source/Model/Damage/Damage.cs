using System;
using Remagures.Model.Flashing;

namespace Remagures.Model.Damage
{
    public sealed class Damage : IDamage 
    {
        public IDamageValue Value { get; }
        
        private readonly FlashingData _flashingData = new(250, 2);

        public Damage(IDamageValue damageValue)
            => Value = damageValue ?? throw new ArgumentNullException(nameof(damageValue));

        public void ApplyTo(ITarget target)
        {
            target.Health.TakeDamage(Value.Get());
            target.Flashingable.Flash(FlashColorType.Damage, FlashColorType.BeforeFlash, _flashingData);
        }
    }
}
