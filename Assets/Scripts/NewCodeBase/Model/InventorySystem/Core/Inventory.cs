using System;
using System.Collections.Generic;
using Remagures.Root;

namespace Remagures.Model.InventorySystem
{
    public class Inventory<T> : IInventory<T>, ILateUpdatable where T: IItem
    { 
        public IReadOnlyList<IReadOnlyCell<T>> Cells => _cells;
        public bool HasCellsChanged { get; private set; }

        private readonly List<ICell<T>> _cells = new();

        public void LateUpdate()
            => HasCellsChanged = false;

        public void Add(ICell<T> newCell)
        {
            if (newCell == null)
                throw new ArgumentNullException(nameof(newCell));
            
            var cellWhichMerging = _cells.Find(cell => cell.Item.Equals(newCell.Item));
            
            if (cellWhichMerging == null)
            {
                _cells.Add(newCell);
                HasCellsChanged = true;
                return;
            }

            if (cellWhichMerging.CanAddItem(newCell.Item))
            {
                cellWhichMerging.Merge(newCell);
                HasCellsChanged = true;
            }
        }

        public void Decrease(ICell<T> decreasingCell)
        {
            if (decreasingCell == null)
                throw new ArgumentNullException(nameof(decreasingCell));
            
            var cellWhichDecreasing = _cells.Find(cell => cell.Item.Equals(decreasingCell.Item));
            
            if (cellWhichDecreasing == null)
                throw new InvalidOperationException("Inventory hasn't item of given cell");
            
            cellWhichDecreasing.DecreaseAmount(decreasingCell.ItemsCount);
            HasCellsChanged = true;
            
            if (cellWhichDecreasing.ItemsCount == 0)
                _cells.Remove(cellWhichDecreasing);
        }
    }
}
