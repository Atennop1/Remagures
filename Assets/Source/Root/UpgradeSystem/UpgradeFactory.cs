using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradeFactory<TItem> : SerializedMonoBehaviour, IUpgradeFactory<TItem> where TItem: IItem
    {
        [SerializeField] private IItemFactory<TItem> _itemFactory;
        [SerializeField] private IUpgradeBuyingData _upgradeBuyingData;
        private IUpgrade<TItem> _builtUpgrade;

        public IUpgrade<TItem> Create()
        {
            if (_builtUpgrade != null)
                return _builtUpgrade;
            
            _builtUpgrade = new Upgrade<TItem>(_itemFactory.Create(), _upgradeBuyingData);
            return _builtUpgrade;
        }
    }
}