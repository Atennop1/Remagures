using System;

namespace Remagures.Model
{
    public sealed class ActivatableArmorValue : IArmorValue
    {
        private readonly IArmorValue _originArmorValue;
        private readonly IArmorValue _armorValueWhichActivating;
        private bool _isActive;

        public ActivatableArmorValue(IArmorValue originArmorValue, IArmorValue armorValueWhichActivating)
        {
            _originArmorValue = originArmorValue ?? throw new ArgumentNullException(nameof(originArmorValue));
            _armorValueWhichActivating = armorValueWhichActivating ?? throw new ArgumentNullException(nameof(armorValueWhichActivating));
        }

        public float Get() 
            => !_isActive ? _originArmorValue.Get() : _armorValueWhichActivating.Get();

        public void Activate()
            => _isActive = true;

        public void Deactivate()
            => _isActive = false;
    }
}