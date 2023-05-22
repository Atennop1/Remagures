using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class WeaponItemFactory : SerializedMonoBehaviour, IItemFactory<IWeaponItem>
    {
        [field: SerializeField] public int ItemID { get; private set; }
        
        [Space]
        [SerializeField] private WeaponItemData _itemData;
        [SerializeField] private IAttackFactory _attackFactory;
        private IWeaponItem _builtItem;
        
        public IWeaponItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_itemData.Name, _itemData.Description, _itemData.Sprite, _itemData.IsStackable);
            _builtItem = new WeaponItem(item, _itemData.AnimatorController, _attackFactory.Create(), _itemData.Damage);
            return _builtItem;
        }
    }
}