using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HelmetItemFactory : MonoBehaviour, IItemFactory<IHelmetItem>
    {
        [SerializeField] private ArmorItemData _data;
        private IHelmetItem _builtItem;

        public IHelmetItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            var armorItem = new ArmorItem(item, _data.AnimatorController, _data.Armor);
            
            _builtItem = new HelmetItem(armorItem);
            return _builtItem;
        }
    }
}
