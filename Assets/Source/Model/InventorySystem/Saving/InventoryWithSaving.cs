using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Root;
using SaveSystem;

namespace Remagures.Model.InventorySystem
{
    public sealed class InventoryWithSaving<TItem> : IInventory<TItem> where TItem : IItem
    {
        public IReadOnlyList<IReadOnlyCell<TItem>> Cells => _inventory.Cells;
        public bool HasCellsChanged => _inventory.HasCellsChanged;
        
        private readonly IInventory<TItem> _inventory;
        private readonly IItemsDatabase<TItem> _itemsDatabase;
        private readonly ISaveStorage<List<CellSavingData>> _cellsDataStorage;

        public InventoryWithSaving(IInventory<TItem> inventory, IItemsDatabase<TItem> itemsDatabase, ISaveStorage<List<CellSavingData>> cellsDataStorage)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _itemsDatabase = itemsDatabase ?? throw new ArgumentNullException(nameof(itemsDatabase));
            _cellsDataStorage = cellsDataStorage ?? throw new ArgumentNullException(nameof(cellsDataStorage));

            if (!_cellsDataStorage.HasSave())
                return;
            
            var loadedCellsData = _cellsDataStorage.Load();
            foreach (var cellData in loadedCellsData)
                _inventory.Add(new Cell<TItem>(_itemsDatabase.GetByID(cellData.ItemID), cellData.ItemsCount));
        }

        public void Add(ICell<TItem> newCell)
        {
            _inventory.Add(newCell);
            _cellsDataStorage.Save(Cells.Select(cell => new CellSavingData(_itemsDatabase.GetItemID(cell.Item), cell.ItemsCount)).ToList());
        }

        public void Remove(ICell<TItem> decreasingCell)
        {
            _inventory.Remove(decreasingCell);
            _cellsDataStorage.Save(Cells.Select(cell => new CellSavingData(_itemsDatabase.GetItemID(cell.Item), cell.ItemsCount)).ToList());
        }
    }
}