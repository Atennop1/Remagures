using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class AddManaPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private int _amount;
        [SerializeField] private IManaFactory _manaFactory;

        public IPickupable Create()
            => new AddManaPickupable(_manaFactory.Create(), _amount);
    }
}