using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradeLevel<TItem> where TItem: IItem
    {
        TItem UpgradedItem { get; }
        UpgradeLevelBuyingData BuyingData { get; }
    }
}