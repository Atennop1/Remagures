using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class AddManaPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private int _amount;
        [SerializeField] private IManaFactory _manaFactory;
        private IPickupable _builtPickupable;

        public IPickupable Create()
        {
            if (_builtPickupable != null)
                return _builtPickupable;

            _builtPickupable = new AddManaPickupable(_manaFactory.Create(), _amount);
            return _builtPickupable;
        } 
    }
}