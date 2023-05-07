using Remagures.Model.InventorySystem;
using Remagures.Model.UI;
using Remagures.View.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class SelectRuneButtonFactory : SerializedMonoBehaviour, IItemButtonFactory<IRuneItem>
    {
        [SerializeField] private ICellView _cellView;
        [SerializeField] private IInventorySelectorFactory<IRuneItem> _inventoryCellSelectorFactory;
        private IItemButton<IRuneItem> _builtButton;
        
        public IItemButton<IRuneItem> Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new SelectRuneButton(_inventoryCellSelectorFactory.Create(), _cellView);
            return _builtButton;
        }
    }
}