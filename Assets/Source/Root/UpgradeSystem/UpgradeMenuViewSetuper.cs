using System.Collections.Generic;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.View.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradeMenuViewSetuper<TItem> : SerializedMonoBehaviour where TItem: IItem
    {
        [SerializeField] private UpgradeMenuView<TItem> _upgradeMenuView;

        [Space]
        [SerializeField] private List<UpgradesChainFactory<TItem>> _upgradeChainFactories;
        [SerializeField] private IInventoryFactory<TItem> _inventoryFactory;

        private void Awake() 
            => _upgradeMenuView.Construct(_upgradeChainFactories.Select(factory => factory.Create()).ToList(), _inventoryFactory.Create());
    }
}