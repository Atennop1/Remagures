using Remagures.Model.InventorySystem;
using Remagures.Model.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TakeMeatButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private CookedMeatHeapFactory _cookedMeatHeapFactory;
        [SerializeField] private IInventoryFactory<IUsableItem> _inventoryFactory;
        [SerializeField] private IItemFactory<IUsableItem> _cookedMeatItemFactory;
        
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new TakeMeatButton(_cookedMeatHeapFactory.Create(), _inventoryFactory.Create(), _cookedMeatItemFactory.Create());
            return _builtButton;
        }
    }
}