using System;
using System.Collections.Generic;

namespace Remagures.Model.InventorySystem
{
    public class Inventory : IInventory
    { 
        public IReadOnlyList<IReadOnlyCell> Cells => _cells;
        
        private readonly List<ICell> _cells = new();

        public void Add(ICell newCell)
        {
            if (newCell == null)
                throw new ArgumentNullException(nameof(newCell));
            
            var cellWhichMerging = _cells.Find(cell => cell.Item == newCell.Item);
            
            if (cellWhichMerging == null)
            {
                _cells.Add(newCell);
                return;
            }
            
            if (cellWhichMerging.CanAddItem(newCell.Item)) 
                cellWhichMerging.Merge(newCell);
        }

        public void Decrease(ICell decreasingCell)
        {
            if (decreasingCell == null)
                throw new ArgumentNullException(nameof(decreasingCell));
            
            var cellWhichDecreasing = _cells.Find(cell => cell.Item == decreasingCell.Item);
            if (cellWhichDecreasing == null)
                throw new InvalidOperationException("Inventory hasn't item of given cell");
            
            cellWhichDecreasing.DecreaseAmount(decreasingCell.ItemsCount);
            if (cellWhichDecreasing.ItemsCount == 0)
                _cells.Remove(cellWhichDecreasing);
        }
    }
}
