using System;
using Remagures.Model.Attacks;
using Remagures.Model.Flashing;
using Remagures.Model.Health;
using Remagures.Model.RuneSystem;
using Remagures.Tools;
using UnityEngine;
using CharacterInfo = Remagures.SO.CharacterInfo;

namespace Remagures.Factories
{
    public sealed class CharacterFireEffectFactory : IHealthEffectFactory
    {
        private readonly int _minFireTime;
        private readonly int _maxFireTime;
        private readonly IRune _fireRune;

        public CharacterFireEffectFactory(int minFireTime, int maxFireTime)
        {
            _minFireTime = minFireTime.ThrowExceptionIfLessOrEqualsZero();
            _maxFireTime = maxFireTime.ThrowExceptionIfLessOrEqualsZero();

            if (_maxFireTime < _minFireTime)
                throw new ArgumentException("MaxFireTime can't be less than MinFireTime");
        }

        public IHealthEffect Create(ITarget target, int damage)
        {
            if (!_fireRune.IsActive)
                return new NullHealthEffect();

            var fireEffect = new FireHealthEffect(target.Health, target.Flashingable, new FireInfo(_minFireTime, _maxFireTime, damage));
            return fireEffect;
        }
    }
}