using Remagures.Model.Buttons;
using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class SelectMagicButtonFactory : SerializedMonoBehaviour, IItemButtonFactory<IMagicItem>
    {
        [SerializeField] private ICellView _cellView;
        [SerializeField] private IInventorySelectorFactory<IMagicItem> _inventoryCellSelectorFactory;
        private IItemButton<IMagicItem> _builtButton;
        
        public IItemButton<IMagicItem> Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new SelectMagicButton(_inventoryCellSelectorFactory.Create(), _cellView);
            return _builtButton;
        }
    }
}