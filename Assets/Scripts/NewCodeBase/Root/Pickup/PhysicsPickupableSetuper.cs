using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsPickupableSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IPickupableFactory _pickupableFactory;
        [SerializeField] private PhysicsPickupable _physicsPickupable;

        private void Awake()
            => _physicsPickupable.Construct(_pickupableFactory.Create());
    }
}