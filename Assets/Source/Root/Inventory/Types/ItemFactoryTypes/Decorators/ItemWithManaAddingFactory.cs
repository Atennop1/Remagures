using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Decorators
{
    public sealed class ItemWithManaAddingFactory : SerializedMonoBehaviour, IItemFactory<IUsableItem>
    {
        [SerializeField] private IItemFactory<IUsableItem> _itemFactory;
        [SerializeField] private IManaFactory _manaFactory;
        [SerializeField] private int _amountToAdd;
        
        private IUsableItem _builtItem;
        public int ItemID => _itemFactory.ItemID;

        public IUsableItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            _builtItem = new ItemWithManaAdding(_itemFactory.Create(), _manaFactory.Create(), _amountToAdd);
            return _builtItem;
        }
    }
}