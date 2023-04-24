using Remagures.Model.InventorySystem;
using Remagures.Model.RuneSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class RunesSelectorFactory : SerializedMonoBehaviour, IInventorySelectorFactory<IRuneItem>
    {
        [SerializeField] private IInventoryFactory<IRuneItem> _runesInventoryFactory;
        [SerializeField] private SelectedRuneView _selectedRuneView;
        
        private RunesSelector _builtSelector;
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();
        
        public IInventoryCellSelector<IRuneItem> Create()
        {
            if (_builtSelector != null)
                return _builtSelector;
            
            _builtSelector = new RunesSelector(_runesInventoryFactory.Create(), _selectedRuneView);
            _lateSystemUpdate.Add(_builtSelector);
            return _builtSelector;
        }
    }
}