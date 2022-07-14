using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{ 
    [SerializeField] [NonReorderable] private List<Cell> _inventory = new List<Cell>();
    public IReadOnlyList<IReadOnlyCell> MyInventory => _inventory;

    public void Add(Cell newCell)
    {
        Cell cell = GetCellInternal(newCell.Item);

        if (cell == null)
            _inventory.Add(newCell);
        else
            cell.Merge(newCell);
    }

    public void Decrease(Cell newCell)
    {
        GetCellInternal(newCell.Item)?.DecreaseAmount(newCell.ItemCount);
    }

    public void Remove(Cell cell)
    {
        if (_inventory.Contains(GetCell(cell.Item)))
            _inventory.Remove(GetCellInternal(cell.Item));
    }

    public bool Contains(IReadOnlyCell cell)
    {
        if (GetCell(cell.Item) != null)
            return _inventory.Contains(GetCell(cell.Item));
        return false;
    }

    public IReadOnlyCell GetCell(BaseInventoryItem item)
    {
        return _inventory.FirstOrDefault(x => x.Item == item);
    }

    private Cell GetCellInternal(BaseInventoryItem item)
    {
        return _inventory.FirstOrDefault(x => x.Item == item);
    }

    public void Clear()
    {
        _inventory.Clear();
    }
}
