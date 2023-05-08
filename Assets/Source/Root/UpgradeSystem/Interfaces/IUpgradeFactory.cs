using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;

namespace Remagures.Root
{
    public interface IUpgradeFactory<TItem> where TItem : IItem
    {
        IUpgrade<TItem> Create();
    }
}