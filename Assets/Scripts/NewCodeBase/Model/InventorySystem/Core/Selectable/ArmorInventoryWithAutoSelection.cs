using System;
using System.Collections.Generic;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class ArmorInventoryWithAutoSelection<T> : IInventoryOfSelectables<T> where T: IArmorItem
    {
        public IReadOnlyList<IReadOnlyCell<T>> Cells => _inventory.Cells;
        public ICell<T> SelectedCell { get; private set; }

        private readonly IInventory<T> _inventory;
        private readonly IInventoryOfSelectablesView _view;

        public ArmorInventoryWithAutoSelection(IInventory<T> inventory, IInventoryOfSelectablesView view)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            AutoSelect();
        }

        private void AutoSelect()
        {
            var biggestArmor = 0;
            ICell<T> strongestArmorCell = null;
            
            foreach (var cell in Cells)
            {
                if (cell.Item.Armor <= biggestArmor) 
                    continue;
                
                biggestArmor = cell.Item.Armor;
                strongestArmorCell = (ICell<T>)cell;
            }

            SelectedCell = strongestArmorCell;
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