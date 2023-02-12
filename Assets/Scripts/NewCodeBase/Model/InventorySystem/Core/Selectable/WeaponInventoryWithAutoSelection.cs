using System;
using System.Collections.Generic;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class WeaponInventoryWithAutoSelection<T> : IInventoryOfSelectables<T> where T: IWeaponItem
    {
        public IReadOnlyList<IReadOnlyCell<T>> Cells => _inventory.Cells;
        public ICell<T> SelectedCell { get; private set; }

        private readonly IInventory<T> _inventory;
        private readonly IInventoryOfSelectablesView _view;

        public WeaponInventoryWithAutoSelection(IInventory<T> inventory, IInventoryOfSelectablesView view)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            AutoSelect();
        }

        private void AutoSelect()
        {
            var biggestDamage = 0;
            ICell<T> strongestWeaponCell = null;
            
            foreach (var cell in Cells)
            {
                if (cell.Item.Damage <= biggestDamage) 
                    continue;
                
                biggestDamage = cell.Item.Damage;
                strongestWeaponCell = (ICell<T>)cell;
            }

            SelectedCell = strongestWeaponCell;
            _view.DisplaySelected((ICell<IItem>)SelectedCell);
        }
        
        public void Add(ICell<T> newCell)
        {
            _inventory.Add(newCell);
            AutoSelect();
        }

        public void Decrease(ICell<T> decreasingCell)
        {
            _inventory.Add(decreasingCell);
            AutoSelect();
        }

        public void Select(T item)
            => AutoSelect();
    }
}