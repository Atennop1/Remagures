using Remagures.Model.InventorySystem;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public interface ICellView
    {
        Button Button { get; }
        void Display(IReadOnlyCell<IItem> cell);
    }
}