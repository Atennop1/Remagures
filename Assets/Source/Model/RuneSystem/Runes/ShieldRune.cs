using System;
using Remagures.Model.Health;

namespace Remagures.Model.RuneSystem
{
    public sealed class ShieldRune : IRune
    {
        public bool IsActive { get; private set; }
        private readonly ActivatableArmorValue _activatableArmorValue;

        public ShieldRune(ActivatableArmorValue activatableArmorValue)
            => _activatableArmorValue = activatableArmorValue ?? throw new ArgumentNullException(nameof(activatableArmorValue));
        
        public void Activate()
        {
            IsActive = true;
            _activatableArmorValue.Activate();
        }

        public void Deactivate()
        {
            IsActive = false;
            _activatableArmorValue.Deactivate();
        }
    }
}