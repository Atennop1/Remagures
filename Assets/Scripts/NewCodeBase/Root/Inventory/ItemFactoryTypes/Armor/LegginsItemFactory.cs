using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class LegginsItemFactory : MonoBehaviour, IItemFactory<ILegginsItem>
    {
        [SerializeField] private ArmorItemData _data;
        private ILegginsItem _builtItem;

        public ILegginsItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            var armorItem = new ArmorItem(item, _data.AnimatorController, _data.Armor);

            _builtItem = new LegginsItem(armorItem);
            return _builtItem;
        }
    }
}
