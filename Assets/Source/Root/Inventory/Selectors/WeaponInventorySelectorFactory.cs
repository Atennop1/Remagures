using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public class WeaponInventorySelectorFactory : SerializedMonoBehaviour, IInventorySelectorFactory<IWeaponItem>
    {
        [SerializeField] private ICellView _selectedCellView;
        [SerializeField] private IDisplayableItemView _displayableItemView;
        [SerializeField] private IInventoryFactory<IWeaponItem> _weaponInventoryFactory;
        
        private AutoWeaponSelector _builtSelector;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();

        private void Update()
            => _systemUpdate.UpdateAll();

        public IInventoryCellSelector<IWeaponItem> Create()
        {
            if (_builtSelector != null)
                return _builtSelector;
            
            _builtSelector = new AutoWeaponSelector(_weaponInventoryFactory.Create(), _selectedCellView);
            var displayableItemApplier = new DisplayableItemApplier<IWeaponItem>(_builtSelector, _displayableItemView);
            
            _systemUpdate.Add(_builtSelector);
            _systemUpdate.Add(displayableItemApplier);
            _lateSystemUpdate.Add(_builtSelector);
            return _builtSelector;
        }
    }
}