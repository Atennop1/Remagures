using Remagures.Model.Interactable;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private string _name;
        [SerializeField] private IInventoryFactory<IItem> _inventoryFactory;
        [SerializeField] private IItemFactory<IItem> _itemFactory;
        
        private IChest _builtChest;

        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;

            _builtChest = new Chest(_inventoryFactory.Create(), _itemFactory.Create(), _name);
            return _builtChest;
        }
    }
}