using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Decorators
{
    public sealed class ItemWithHealthAddingFactory : SerializedMonoBehaviour, IItemFactory<IUsableItem>
    {
        [SerializeField] private IItemFactory<IUsableItem> _itemFactory;
        [SerializeField] private HealthFactory _healthFactory;
        [SerializeField] private int _amountToAdd;
        
        private IUsableItem _builtItem;
        public int ItemID => _itemFactory.ItemID;

        public IUsableItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            _builtItem = new ItemWithHealthAdding(_itemFactory.Create(), _healthFactory.Create(), _amountToAdd);
            return _builtItem;
        }
    }
}