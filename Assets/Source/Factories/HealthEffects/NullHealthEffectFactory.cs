﻿using Remagures.Model.Damage;
using Remagures.Model.Health;

namespace Remagures.Factories
{
    public sealed class NullHealthEffectFactory : IHealthEffectFactory
    {
        public IHealthEffect Create(ITarget target, int damage)
            => new NullHealthEffect();
    }
}