using System;
using Remagures.Model.Health;

namespace Remagures.Model.RuneSystem
{
    public sealed class ShieldRune : IRune
    {
        public bool IsActive { get; private set; }
        private readonly ActivatableArmor _activatableArmor;

        public ShieldRune(ActivatableArmor activatableArmor)
            => _activatableArmor = activatableArmor ?? throw new ArgumentNullException(nameof(activatableArmor));
        
        public void Activate()
        {
            IsActive = true;
            _activatableArmor.Activate();
        }

        public void Deactivate()
        {
            IsActive = false;
            _activatableArmor.Deactivate();
        }
    }
}