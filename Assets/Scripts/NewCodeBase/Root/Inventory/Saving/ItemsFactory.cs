using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public class ItemsFactory<TItem> : SerializedMonoBehaviour where TItem: IItem
    {
        [SerializeField] private List<IItemFactory<TItem>> _factories;
        private IItems<TItem> _builtItems;

        public IItems<TItem> Create()
        {
            if (_builtItems != null)
                return _builtItems;

            _builtItems = new Items<TItem>(_factories);
            return _builtItems;
        }
    }
}