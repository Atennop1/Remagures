using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesChain<TItem> where TItem: IItem
    {
        bool CanUpgradeItem(TItem item);
        ItemUpgradeData<TItem> GetUpgradeForItem(TItem item);
    }
}