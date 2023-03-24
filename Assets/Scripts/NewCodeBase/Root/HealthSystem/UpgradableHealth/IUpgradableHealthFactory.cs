using Remagures.Model.Health;

namespace Remagures.Root
{
    public interface IUpgradableHealthFactory
    {
        IUpgradableHealth Create();
    }
}