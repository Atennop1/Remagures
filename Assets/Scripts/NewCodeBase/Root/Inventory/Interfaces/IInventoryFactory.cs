using Remagures.Model.InventorySystem;

namespace Remagures.Root
{
    public interface IInventoryFactory<TItem> where TItem: IItem
    {
        IInventory<TItem> Create();
    }
}