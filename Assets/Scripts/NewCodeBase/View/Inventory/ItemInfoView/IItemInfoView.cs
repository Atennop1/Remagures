using Remagures.Model.InventorySystem;

namespace Remagures.View.Inventory
{
    public interface IItemInfoView<T> where T: IItem
    {
        void Display(T item);
    }
}