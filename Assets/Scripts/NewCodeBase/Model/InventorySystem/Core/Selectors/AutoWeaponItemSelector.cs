using System;
using System.Linq;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class AutoWeaponItemSelector<T> : IInventoryItemSelector<T> where T: IWeaponItem
    {
        public IReadOnlyCell<T> SelectedCell { get; private set; }

        private readonly IInventory<T> _inventory;
        private readonly ICellView _selectedCellView;

        public AutoWeaponItemSelector(IInventory<T> inventory, ICellView selectedCellView)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _selectedCellView = selectedCellView ?? throw new ArgumentNullException(nameof(selectedCellView));
            AutoSelect();
        }

        private void AutoSelect()
        {
            if (_inventory.Cells.Count == 0)
                return;
            
            var cellsList = _inventory.Cells.ToList();
            var biggestDamage = cellsList.Max(cell => cell.Item.Damage);
            
            SelectedCell = cellsList.Find(cell => cell.Item.Damage == biggestDamage);
            _selectedCellView.Display((ICell<IItem>)SelectedCell);
        }

        public void Select(T item) { }
        public void UnSelect() { }
    }
}