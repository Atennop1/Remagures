using Remagures.Model.InventorySystem;
using Remagures.Model.MeatSystem;
using Remagures.View.MeatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class CookedMeatHeapFactory : SerializedMonoBehaviour
    {
        [SerializeField] private MeatCountView _meatCountView;
        [SerializeField] private IInventoryFactory<IItem> _meatInventoryFactory;
        [SerializeField] private IItemFactory<IItem> _cookedMeatItemFactory;

        private ICookedMeatHeap _builtHeap;

        public ICookedMeatHeap Create()
        {
            if (_builtHeap != null)
                return _builtHeap;
            
            _builtHeap = new CookedMeatHeap(_meatCountView, _meatInventoryFactory.Create(), _cookedMeatItemFactory.Create());
            return _builtHeap;
        }
    }
}