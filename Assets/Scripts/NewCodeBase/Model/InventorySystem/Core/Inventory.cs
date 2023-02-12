using System;
using System.Collections.Generic;

namespace Remagures.Model.InventorySystem
{
    public class Inventory<T> : IInventory<T> where T: IItem
    { 
        public IReadOnlyList<IReadOnlyCell<T>> Cells => _cells;
        
        private readonly List<ICell<T>> _cells = new();

        public void Add(ICell<T> newCell)
        {
            if (newCell == null)
                throw new ArgumentNullException(nameof(newCell));
            
            var cellWhichMerging = _cells.Find(cell => cell.Item.GetType() == newCell.Item.GetType() 
                                                       && (IItem)cell.Item == (IItem)newCell.Item);
            
            if (cellWhichMerging == null)
            {
                _cells.Add(newCell);
                return;
            }
            
            if (cellWhichMerging.CanAddItem(newCell.Item)) 
                cellWhichMerging.Merge(newCell);
        }

        public void Decrease(ICell<T> decreasingCell)
        {
            if (decreasingCell == null)
                throw new ArgumentNullException(nameof(decreasingCell));
            
            var cellWhichDecreasing = _cells.Find(cell => cell.Item.GetType() == decreasingCell.Item.GetType() 
                                                          && (IItem)cell.Item == (IItem)decreasingCell.Item);
            
            if (cellWhichDecreasing == null)
                throw new InvalidOperationException("Inventory hasn't item of given cell");
            
            cellWhichDecreasing.DecreaseAmount(decreasingCell.ItemsCount);
            if (cellWhichDecreasing.ItemsCount == 0)
                _cells.Remove(cellWhichDecreasing);
        }
    }
}
