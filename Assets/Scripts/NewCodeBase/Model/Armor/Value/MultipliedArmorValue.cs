using System;
using Remagures.Tools;

namespace Remagures.Model
{
    public sealed class MultipliedArmorValue : IArmorValue
    {
        private readonly IArmorValue _armorValue;
        private readonly float _multiplier;

        public MultipliedArmorValue(IArmorValue armorValue, float multiplier)
        {
            _armorValue = armorValue ?? throw new ArgumentNullException(nameof(armorValue));
            _multiplier = multiplier.ThrowExceptionIfLessOrEqualsZero();
        }

        public float Get() 
            => _armorValue.Get() * _multiplier;
    }
}