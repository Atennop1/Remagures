using System;
using Remagures.Tools;

namespace Remagures.Model.Health
{
    public sealed class ActivatableArmor : IArmor
    {
        private readonly IArmor _armor;
        private readonly float _armorCoefficient;
        
        private bool _isActivated;

        public ActivatableArmor(IArmor armor, float armorCoefficient)
        {
            _armor = armor ?? throw new ArgumentNullException(nameof(armor));
            _armorCoefficient = armorCoefficient.ThrowExceptionIfLessOrEqualsZero();
        }

        public int AbsorbDamage(int damage)
        {
            if (!_isActivated) 
                return _armor.AbsorbDamage(damage);
            
            var totalDamage = (int)(_armor.AbsorbDamage(damage) * _armorCoefficient);
                
            if (totalDamage == 0)
                totalDamage = 1;

            return totalDamage;
        }

        public void Activate()
            => _isActivated = true;

        public void Deactivate()
            => _isActivated = false;
    }
}