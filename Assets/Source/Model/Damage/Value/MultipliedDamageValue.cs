using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.Damage
{
    public sealed class MultipliedDamageValue : IDamageValue
    {
        private readonly IDamageValue _damageValue;
        private readonly float _multiplier;

        public MultipliedDamageValue(IDamageValue damageValue, float multiplier)
        {
            _damageValue = damageValue ?? throw new ArgumentNullException(nameof(damageValue));
            _multiplier = multiplier.ThrowExceptionIfLessOrEqualsZero();
        }

        public int Get() 
            => Mathf.RoundToInt(_damageValue.Get() * _multiplier);
    }
}