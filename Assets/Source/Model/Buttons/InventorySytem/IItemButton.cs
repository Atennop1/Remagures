using Remagures.Model.InventorySystem;

namespace Remagures.Model.Buttons
{
    public interface IItemButton<T> : IButton where T: IItem
    {
        void SetItem(T item);
    }
}