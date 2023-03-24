using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MagicItemFactory : MonoBehaviour, IItemFactory<IMagicItem> //TODO make composite for magic and fix this
    {
        [SerializeField] private MagicItemData _data;
        private IMagicItem _builtItem;
        
        public IMagicItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            _builtItem = new MagicItem(item, _data.UsingCooldownInMilliseconds);
            return _builtItem;
        }
    }
}