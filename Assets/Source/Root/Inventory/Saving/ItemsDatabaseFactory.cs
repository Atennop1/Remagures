using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public class ItemsDatabaseFactory<TItem> : SerializedMonoBehaviour where TItem: IItem
    {
        [SerializeField] private List<IItemFactory<TItem>> _factories;
        private IItemsDatabase<TItem> _builtItemsDatabase;

        public IItemsDatabase<TItem> Create()
        {
            if (_builtItemsDatabase != null)
                return _builtItemsDatabase;

            _builtItemsDatabase = new ItemsDatabase<TItem>(_factories);
            return _builtItemsDatabase;
        }
    }
}