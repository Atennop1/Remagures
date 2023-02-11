namespace Remagures.Model.InventorySystem
{
    public interface IReadOnlyCell
    {
        int ItemsCount { get; }
        IItem Item { get; }
        bool CanAddItem(IItem item);
    }
}