using Remagures.Model.InventorySystem;

namespace Remagures.View.Inventory
{
    public interface ICellView
    {
        void Display(ICell<IItem> cell);
    }
}