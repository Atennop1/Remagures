using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ItemFactory : MonoBehaviour, IItemFactory<IItem>
    {
        [SerializeField] private ItemData _data;
        private IItem _builtItem;
        
        public IItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            _builtItem = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            return _builtItem;
        }
    }
}