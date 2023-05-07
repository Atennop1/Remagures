using Remagures.Model.InventorySystem;
using Remagures.Model.UI;
using Remagures.View.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UseItemButtonFactory : SerializedMonoBehaviour, IItemButtonFactory<IUsableItem>
    {
        [SerializeField] private IInventoryFactory<IUsableItem> _inventoryFactory;
        [SerializeField] private ItemInfoView<IUsableItem> _itemInfoView;
        [SerializeField] private IItemFactory<IItem> _nullItemFactory;
        private IItemButton<IUsableItem> _builtButton;
        
        public IItemButton<IUsableItem> Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new UseItemButton(_inventoryFactory.Create(), _itemInfoView, _nullItemFactory);
            return _builtButton;
        }
    }
}