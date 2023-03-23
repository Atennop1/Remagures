using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesChain<TItem> where TItem: IItem
    {
        bool CanUpgrade(TItem item);
        
        void Upgrade(TItem item);
        IUpgrade<TItem> GetNextLevel(TItem item);
    }
}