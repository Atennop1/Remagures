using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorInventorySelectorFactory : SerializedMonoBehaviour, IInventorySelectorFactory<IArmorItem>
    {
        [SerializeField] private ICellView _selectedCellView;
        [SerializeField] private IDisplayableItemView _displayableItemView;
        [SerializeField] private IInventoryFactory<IArmorItem> _armorInventoryFactory;
        
        private AutoArmorSelector _builtSelector;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();

        private void Update()
            => _systemUpdate.UpdateAll();

        public IInventoryCellSelector<IArmorItem> Create()
        {
            if (_builtSelector != null)
                return _builtSelector;
            
            _builtSelector = new AutoArmorSelector(_armorInventoryFactory.Create(), _selectedCellView);
            var displayableItemApplier = new DisplayableItemApplier<IArmorItem>(_builtSelector, _displayableItemView);
            
            _systemUpdate.Add(_builtSelector);
            _systemUpdate.Add(displayableItemApplier);
            _lateSystemUpdate.Add(_builtSelector);
            
            return _builtSelector;
        }
    }
}