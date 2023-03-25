using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class InventoryWithSavingFactory<TItem> : SerializedMonoBehaviour, IInventoryFactory<TItem> where TItem: IItem
    {
        [SerializeField] private IInventoryFactory<TItem> _inventoryFactory;
        [SerializeField] private ItemsFactory<TItem> _itemsFactory;
        private IInventory<TItem> _builtInventory;

        public IInventory<TItem> Create()
        {
            if (_builtInventory != null)
                return _builtInventory;

            var storage = new BinaryStorage<List<CellSavingData>>(new Path($"{nameof(TItem)}-Inventory"));
            _builtInventory = new InventoryWithSaving<TItem>(_inventoryFactory.Create(), _itemsFactory.Create(), storage);
            return _builtInventory;
        }
    }
}