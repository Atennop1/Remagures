using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradeLevel<TItem> where TItem: IItem
    {
        TItem CurrentItem { get; }
        TItem NextItem { get; }
        UpgradeLevelBuyingData BuyingData { get; }
    }
}