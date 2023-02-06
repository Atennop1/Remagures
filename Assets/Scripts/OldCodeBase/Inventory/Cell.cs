using System;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Inventory
{
    [Serializable]
    public class Cell : IReadOnlyCell
    {
        [field: SerializeField] public int ItemCount { get; private set; }
        [field: SerializeField] public BaseInventoryItem Item { get; private set; }

        public Cell(BaseInventoryItem item, int count = 1)
        {
            ItemCount = count;
            Item = item;
        }

        public void Merge(Cell anotherCell)
        {
            if (!CanMergeWithItem(anotherCell.Item))
                throw new ArgumentException("Can't merge 2 cells with different items!");
            
            if (!CanAddItemAmount())
                throw new ArgumentException("Can't merge non-stackable cell!");
            
            ItemCount += anotherCell.ItemCount;
        }
        
        public bool CanMergeWithItem(BaseInventoryItem item) 
            => item == Item;
        
        public bool CanAddItemAmount() 
            => Item.Stackable || ItemCount == 0;
 
        public void DecreaseAmount(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Can't reduce item quantity by negative or zero value");
            
            if (!ItemCountGreaterOrEqualValue(amount))
                throw new ArgumentException("The number of items is less than the requested value");
                
            ItemCount -= amount;
        }

        public bool ItemCountGreaterOrEqualValue(int value) 
            => ItemCount >= value;
    }
}