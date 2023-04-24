using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgrade<TItem> where TItem: IItem
    {
        TItem Item { get; }
        IUpgradeBuyingData BuyingData { get; }
    }
}