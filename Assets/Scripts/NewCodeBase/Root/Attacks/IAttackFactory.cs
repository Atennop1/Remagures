using Remagures.Model.Attacks;

namespace Remagures.Root
{
    public interface IAttackFactory
    {
        IAttack Create();
    }
}