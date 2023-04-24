using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public class InventoryFactory<TItem> : SerializedMonoBehaviour, IInventoryFactory<TItem> where TItem: IItem
    {
        private Inventory<TItem> _builtInventory;
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();

        public IInventory<TItem> Create()
        {
            if (_builtInventory != null)
                return _builtInventory;
            
            _builtInventory = new Inventory<TItem>();
            _lateSystemUpdate.Add(_builtInventory);
            return _builtInventory;
        }
    }
}