using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;

namespace Remagures.Root
{
    public interface IUpgradesChainFactory<TItem> where TItem : IItem
    {
        IUpgradesChain<TItem> Create();
    }
}