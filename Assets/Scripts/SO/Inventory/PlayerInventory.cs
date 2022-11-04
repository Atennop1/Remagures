using System.Collections.Generic;
using System.Linq;
using Remagures.Inventory;
using Remagures.SO.Inventory.Items;
using UnityEngine;

namespace Remagures.SO.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/PlayerInventory")]
    public class PlayerInventory : ScriptableObject
    { 
        [SerializeField] [NonReorderable] private List<Cell> _inventory = new();
        public IReadOnlyList<IReadOnlyCell> MyInventory => _inventory;

        public void Add(Cell newCell)
        {
            var cell = GetCellInternal(newCell.Item);

            if (cell == null) _inventory.Add(newCell);
            else cell.Merge(newCell);
        }
        
        public void Remove(Cell cell)
        {
            if (_inventory.Contains(GetCell(cell.Item)))
                _inventory.Remove(GetCellInternal(cell.Item));
        }

        public void Decrease(Cell newCell) => GetCellInternal(newCell.Item)?.DecreaseAmount(newCell.ItemCount);
        public bool Contains(IReadOnlyCell cell) => GetCell(cell.Item) != null && _inventory.Contains(GetCell(cell.Item));
        public IReadOnlyCell GetCell(BaseInventoryItem item) => _inventory.FirstOrDefault(x => x.Item == item);
        public void Clear() => _inventory.Clear();
        private Cell GetCellInternal(BaseInventoryItem item) => _inventory.FirstOrDefault(x => x.Item == item);
    }
}
