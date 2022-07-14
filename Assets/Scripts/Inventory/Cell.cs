using UnityEngine;
using System;

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
        if (anotherCell.Item == Item && Item.Stackable)
        {
            ItemCount += anotherCell.ItemCount;
        }
        else if (anotherCell.Item == Item)
        {
            if (ItemCount == 1)
                throw new ArgumentException("Can't merge non-stakable cell!");
            else
                ItemCount = 1;
        }
        else if (anotherCell.Item != Item)
        {
            throw new ArgumentException("Can't merge 2 cells with different items!");
        }
    }

    public void DecreaseAmount(int amount)
    {
        ItemCount -= amount;
        if (ItemCount < 0)
            ItemCount = 0;
    }
}

public interface IReadOnlyCell
{
    public int ItemCount { get; }
    public BaseInventoryItem Item { get; }
}
