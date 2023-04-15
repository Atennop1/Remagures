namespace Remagures.Model.InventorySystem
{
    public interface IItemsDatabase<TItem>
    {
        int GetItemID(TItem item);
        TItem GetByID(int id);
    }
}