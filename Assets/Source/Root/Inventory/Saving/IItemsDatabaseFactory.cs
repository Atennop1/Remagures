using Remagures.Model.InventorySystem;

namespace Remagures.Root
{
    public interface IItemsDatabaseFactory<TItem> where TItem : IItem
    {
        IItemsDatabase<TItem> Create();
    }
}