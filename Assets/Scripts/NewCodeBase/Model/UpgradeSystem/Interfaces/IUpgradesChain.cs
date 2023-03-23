using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesChain<TItem> where TItem: IItem
    {
        bool CanAdvance(TItem item);
        
        void Advance(TItem item);
        IUpgradeLevel<TItem> GetCurrentLevel(TItem item);
    }
}