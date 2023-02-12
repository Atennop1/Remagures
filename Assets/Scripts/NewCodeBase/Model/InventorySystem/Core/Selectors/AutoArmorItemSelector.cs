using System;
using System.Linq;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class AutoArmorItemSelector<T> : IInventoryItemSelector<T>, IUpdatable where T: IArmorItem
    {
        public IReadOnlyCell<T> SelectedCell { get; private set; }

        private readonly IInventory<T> _inventory;
        private readonly ICellView _selectedCellView;

        public AutoArmorItemSelector(IInventory<T> inventory, ICellView selectedCellView)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _selectedCellView = selectedCellView ?? throw new ArgumentNullException(nameof(selectedCellView));
            AutoSelect();
        }
        
        public void Update()
        {
            if (_inventory.HasCellsChanged)
                AutoSelect();
        }

        private void AutoSelect()
        {
            if (_inventory.Cells.Count == 0)
                return;
            
            var cellsList = _inventory.Cells.ToList();
            var biggestArmor = cellsList.Max(cell => cell.Item.Armor);
            
            SelectedCell = cellsList.Find(cell => cell.Item.Armor == biggestArmor);
            _selectedCellView.Display((ICell<IItem>)SelectedCell);
        }

        public void Select(T item) { }
        public void UnSelect() { }
    }
}