using System;
using Remagures.Tools;

namespace Remagures.Model.InventorySystem
{
    [Serializable]
    public class Cell : ICell
    {
        public int ItemsCount { get; private set; }
        public IItem Item { get; private set; }

        public Cell(IItem item, int count = 1)
        {
            ItemsCount = count.ThrowExceptionIfLessOrEqualsZero();
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
        
        public bool CanAddItem(IItem item)
            => CanMergeWithItem(item) && Item.IsStackable || ItemsCount == 0;

        public void Merge(ICell anotherCell)
        {
            if (anotherCell == null)
                throw new ArgumentNullException(nameof(anotherCell));

            if (CanMergeWithItem(anotherCell.Item))
                throw new ArgumentException("Can't merge 2 cells with different items!");
            
            if (!Item.IsStackable && ItemsCount != 0)
                throw new ArgumentException("Can't merge non-stackable cell!");
            
            ItemsCount += anotherCell.ItemsCount;
        }

        public void DecreaseAmount(int amount)
        {
            if (amount.ThrowExceptionIfLessOrEqualsZero() > ItemsCount)
                throw new ArgumentException("The number of items is less than the requested value");
                
            ItemsCount -= amount;
        }

        private bool CanMergeWithItem(IItem item)
            => Item == item;
    }
}