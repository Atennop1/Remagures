using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestplateItemFactory : MonoBehaviour, IItemFactory<IChestplateItem>
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [SerializeField] private ArmorItemData _data;
        private IChestplateItem _builtItem;

        public IChestplateItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            var armorItem = new ArmorItem(item, _data.AnimatorController, _data.Armor);

            _builtItem = new ChestplateItem(armorItem);
            return _builtItem;
        }
    }
}