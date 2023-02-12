namespace Remagures.Model.InventorySystem
{
    public interface IInventoryOfSelectables<T> : IInventory<T> where T: IItem
    {
        ICell<T> SelectedCell { get; }
        void Select(T item);
    }
}