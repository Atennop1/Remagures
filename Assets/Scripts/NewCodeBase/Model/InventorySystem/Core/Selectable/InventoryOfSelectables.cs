using System;
using System.Collections.Generic;
using System.Linq;

namespace Remagures.Model.InventorySystem
{
    public class InventoryOfSelectables<T> : IInventoryOfSelectables<T> where T: IItem
    {
        public IReadOnlyList<IReadOnlyCell<T>> Cells => _inventory.Cells;
        public ICell<T> SelectedCell { get; private set; }

        private readonly IInventory<T> _inventory;

        public void Select(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var cellInInventory = Cells.ToList().Find(cell => cell.Item.GetType() == item.GetType() 
                                                      && (IItem)cell.Item == (IItem)item);

            SelectedCell = (ICell<T>)cellInInventory 
                           ?? throw new InvalidOperationException("Inventory doesn't contains this item");
        }
        
        public void Add(ICell<T> newCell)
            => _inventory.Add(newCell);

        public void Decrease(ICell<T> decreasingCell)
            => _inventory.Decrease(decreasingCell);
    }
}