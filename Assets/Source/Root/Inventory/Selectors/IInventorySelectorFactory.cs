using Remagures.Model.InventorySystem;

namespace Remagures.Root
{
    public interface IInventorySelectorFactory<TItem> where TItem: IItem
    {
        IInventoryCellSelector<TItem> Create();
    }
}