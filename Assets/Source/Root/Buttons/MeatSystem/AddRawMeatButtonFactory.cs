using Remagures.Model.Buttons;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class AddRawMeatButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private IMeatCookerFactory _meatCookerFactory;
        [SerializeField] private IInventoryFactory<IItem> _inventoryFactory;
        [SerializeField] private IItemFactory<IItem> _rawMeatItemFactory;
        
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new AddRawMeatButton(_meatCookerFactory.Create(), _inventoryFactory.Create(), _rawMeatItemFactory.Create());
            return _builtButton;
        }
    }
}