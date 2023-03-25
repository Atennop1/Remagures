using System;
using System.Linq;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class AutoWeaponSelector : IInventoryCellSelector<IWeaponItem>, IUpdatable, ILateUpdatable
    {
        public IReadOnlyCell<IWeaponItem> SelectedCell { get; private set; }
        public bool HasSelected { get; private set; }

        private readonly IInventory<IWeaponItem> _inventory;
        private readonly ICellView _selectedCellView;

        public AutoWeaponSelector(IInventory<IWeaponItem> inventory, ICellView selectedCellView)
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
            var biggestDamage = cellsList.Max(cell => cell.Item.Damage);
            
            HasSelected = true;
            SelectedCell = cellsList.Find(cell => cell.Item.Damage == biggestDamage);
            _selectedCellView.Display(SelectedCell as ICell<IItem>);
        }

        public void Select(IWeaponItem item) { }
        public void UnSelect() { }
    }
}