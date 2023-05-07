using Remagures.Model.MeatSystem;
using Remagures.View.MeatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class CookedMeatHeapFactory : SerializedMonoBehaviour
    {
        [SerializeField] private MeatCountView _meatCountView;
        private ICookedMeatHeap _builtHeap;

        public ICookedMeatHeap Create()
        {
            if (_builtHeap != null)
                return _builtHeap;
            
            _builtHeap = new CookedMeatHeap(_meatCountView);
            return _builtHeap;
        }
    }
}