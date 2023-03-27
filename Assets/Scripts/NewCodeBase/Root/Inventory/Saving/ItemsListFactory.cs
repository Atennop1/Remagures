using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public class ItemsListFactory<TItem> : SerializedMonoBehaviour where TItem: IItem
    {
        [SerializeField] private List<IItemFactory<TItem>> _factories;
        private IItemsList<TItem> _builtItemsList;

        public IItemsList<TItem> Create()
        {
            if (_builtItemsList != null)
                return _builtItemsList;

            _builtItemsList = new ItemsList<TItem>(_factories);
            return _builtItemsList;
        }
    }
}