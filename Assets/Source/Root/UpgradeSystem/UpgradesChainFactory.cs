using System.Collections.Generic;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradesChainFactory<TItem> : SerializedMonoBehaviour, IUpgradesChainFactory<TItem> where TItem: IItem
    {
        [SerializeField] private Dictionary<IItemFactory<TItem>, IUpgradeFactory<TItem>> _upgradeFactories;
        [SerializeField] private IInventoryFactory<TItem> _inventoryFactory;
        [SerializeField] private IUpgradesClientFactory _upgradesClientFactory;

        private IUpgradesChain<TItem> _builtChain;

        public IUpgradesChain<TItem> Create()
        {
            if (_builtChain != null)
                return _builtChain;

            var upgradesDictionary = _upgradeFactories.ToDictionary(pair => pair.Key.Create(), pair => pair.Value.Create());
            _builtChain = new UpgradesChain<TItem>(upgradesDictionary, _inventoryFactory.Create(), _upgradesClientFactory.Create());
            return _builtChain;
        }
    }
}