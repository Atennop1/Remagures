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
        private IInventoryCellSelector<IRuneItem> _builtSelector;
        
        public IInventoryCellSelector<IRuneItem> Create()
        {
            if (_builtSelector != null)
                return _builtSelector;
            
            _builtSelector = new RunesSelector(_runesInventoryFactory.Create(), _selectedRuneView);
            return _builtSelector;
        }
    }
}