using System;

namespace Remagures.Model.Damage
{
    public sealed class ActivatableDamageValue : IDamageValue
    {
        private readonly IDamageValue _originDamageValue;
        private readonly IDamageValue _damageValueWhichActivating;
        private bool _isActive;

        public ActivatableDamageValue(IDamageValue originDamageValue, IDamageValue damageValueWhichActivating)
        {
            _originDamageValue = originDamageValue ?? throw new ArgumentNullException(nameof(originDamageValue));
            _damageValueWhichActivating = damageValueWhichActivating ?? throw new ArgumentNullException(nameof(damageValueWhichActivating));
        }

        public int Get() 
            => !_isActive ? _originDamageValue.Get() : _damageValueWhichActivating.Get();

        public void Activate()
            => _isActive = true;

        public void Deactivate()
            => _isActive = false;
    }
}