using System;
using Remagures.Model.Attacks;
using Remagures.Model.Flashing;
using Remagures.Model.Health;
using Remagures.Tools;
using UnityEngine;
using CharacterInfo = Remagures.SO.CharacterInfo;

namespace Remagures.Factories
{
    public sealed class CharacterFireEffectFactory : IHealthEffectFactory
    {
        private readonly int _minFireTime;
        private readonly int _maxFireTime;
        private readonly CharacterInfo _characterInfo;

        public CharacterFireEffectFactory(int minFireTime, int maxFireTime)
        {
            _minFireTime = minFireTime.ThrowExceptionIfLessOrEqualsZero();
            _maxFireTime = maxFireTime.ThrowExceptionIfLessOrEqualsZero();

            if (_maxFireTime < _minFireTime)
                throw new ArgumentException("MaxFireTime can't be less than MinFireTime");
        }

        public IHealthEffect Create(Target target, int damage)
        {
            if (!_characterInfo.FireRunActive)
                return new NullHealthEffect();

            var fireEffect = new FireHealthEffect(target.Health, target.Flashingable, new FireInfo(_minFireTime, _maxFireTime, damage));
            return fireEffect;
        }
    }
}