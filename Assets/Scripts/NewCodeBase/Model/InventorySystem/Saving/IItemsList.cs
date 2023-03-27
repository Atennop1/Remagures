namespace Remagures.Model.InventorySystem
{
    public interface IItemsList<TItem>
    {
        int GetItemID(TItem item);
        TItem GetByID(int id);
    }
}