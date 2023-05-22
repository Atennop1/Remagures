using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MagicItemFactory : SerializedMonoBehaviour, IItemFactory<IMagicItem> 
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [SerializeField] private ItemData _data;
        [SerializeField] private IMagicFactory _magicFactory;
        private IMagicItem _builtItem;
        
        public IMagicItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            _builtItem = new MagicItem(item, _magicFactory.Create());
            return _builtItem;
        }
    }
}