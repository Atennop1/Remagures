using Remagures.Model.InventorySystem;
using Remagures.Model.MeatSystem;
using Remagures.View.MeatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MeatCookerFactory : SerializedMonoBehaviour, IMeatCookerFactory
    {
        [SerializeField] private MeatCountView _meatCountView;
        [SerializeField] private IInventoryFactory<IItem> _meatInventoryFactory;
        
        [SerializeField] private IItemFactory<IItem> _rawMeatItemFactory;
        [SerializeField] private IItemFactory<IItem> _cookedMeatItemFactory;

        private IMeatCooker _builtMeatCooker;

        public IMeatCooker Create()
        {
            if (_builtMeatCooker != null)
                return _builtMeatCooker;
                
            _builtMeatCooker = new MeatCooker(_meatCountView, _meatInventoryFactory.Create(), _cookedMeatItemFactory.Create(), _rawMeatItemFactory.Create());
            return _builtMeatCooker;
        }
    }
}