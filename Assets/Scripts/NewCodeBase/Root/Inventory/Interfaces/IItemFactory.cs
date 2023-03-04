using Remagures.Model.InventorySystem;

namespace Remagures.Root
{
    public interface IItemFactory<TItem> where TItem: IItem
    {
        TItem Create();
    }
}