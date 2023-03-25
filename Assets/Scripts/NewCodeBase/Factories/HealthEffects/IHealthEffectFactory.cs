using Remagures.Model.Damage;
using Remagures.Model.Health;

namespace Remagures.Factories
{
    public interface IHealthEffectFactory
    {
        IHealthEffect Create(ITarget target, int damage);
    }
}