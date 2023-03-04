using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public class InventoryFactory<TItem> : SerializedMonoBehaviour, IInventoryFactory<TItem> where TItem: IItem
    {
        private IInventory<TItem> _builtInventory;

        public IInventory<TItem> Create()
        {
            if (_builtInventory != null)
                return _builtInventory;
            
            _builtInventory = new Inventory<TItem>();
            return _builtInventory;
        }
    }
}