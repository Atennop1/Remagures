using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class WeaponItemFactory : MonoBehaviour, IItemFactory<IWeaponItem>
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [SerializeField] private WeaponItemData _data;
        private IWeaponItem _builtItem;
        
        public IWeaponItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            _builtItem = new WeaponItem(item, _data.AnimatorController, _data.Damage, _data.UsingCooldownInMilliseconds, _data.AttackingTimeInMilliseconds);
            return _builtItem;
        }
    }
}