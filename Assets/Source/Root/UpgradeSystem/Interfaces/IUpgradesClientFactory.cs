using Remagures.Model.UpgradeSystem;

namespace Remagures.Root
{
    public interface IUpgradesClientFactory
    {
        IUpgradesClient Create();
    }
}