using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.View.Inventory
{
    public class InventoryOfSelectablesView : MonoBehaviour, IInventoryOfSelectablesView
    {
        [SerializeField] private CellView cellView;
        
        public void DisplaySelected(ICell<IItem> cell)
        {
            cellView.Setup(cell);
        }
    }
}