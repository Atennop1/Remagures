using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UsableItemFactory : SerializedMonoBehaviour, IItemFactory<IUsableItem>
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [SerializeField] private ItemData _data;
        private IUsableItem _builtItem;
        
        public IUsableItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            _builtItem = new UsableItem(item);
            return _builtItem;
        }
    }
}