using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;
using UnityEngine;

namespace Remagures.Root
{
    public class ArmorInventorySelectorFactory : MonoBehaviour, IInventorySelectorFactory<IArmorItem>
    {
        [SerializeField] private ICellView _selectedCellView;
        [SerializeField] private IDisplayableItemView _displayableItemView;
        [SerializeField] private IInventoryFactory<IArmorItem> _armorInventoryFactory;
        
        private AutoArmorSelector _builtSelector;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

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
            return _builtSelector;
        }
    }
}