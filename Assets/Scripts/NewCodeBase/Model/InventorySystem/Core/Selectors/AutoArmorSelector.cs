using System;
using System.Linq;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class AutoArmorSelector : IInventoryCellSelector<IArmorItem>, IUpdatable, ILateUpdatable
    {
        public IReadOnlyCell<IArmorItem> SelectedCell { get; private set; }
        public bool HasSelected { get; private set; }

        private readonly IInventory<IArmorItem> _inventory;
        private readonly ICellView _selectedCellView;

        public AutoArmorSelector(IInventory<IArmorItem> inventory, ICellView selectedCellView)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _selectedCellView = selectedCellView ?? throw new ArgumentNullException(nameof(selectedCellView));
            AutoSelect();
        }
        
        public void LateUpdate()
            => HasSelected = false;
        
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
            _selectedCellView.Display(SelectedCell as ICell<IItem>);
        }

        public void Select(IArmorItem item) { }
        public void UnSelect() { }
    }
}