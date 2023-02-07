using Remagures.Model.Attacks;
using Remagures.Model.Health;

namespace Remagures.Factories
{
    public sealed class NullHealthEffectFactory : IHealthEffectFactory
    {
        public IHealthEffect Create(Target target, int damage)
            => new NullHealthEffect();
    }
}