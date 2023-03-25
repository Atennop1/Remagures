namespace Remagures.Model.InventorySystem
{
    public interface IItems<TItem>
    {
        int GetItemID(TItem item);
        TItem GetByID(int id);
    }
}