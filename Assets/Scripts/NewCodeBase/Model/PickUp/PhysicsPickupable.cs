using System;
using UnityEngine;

namespace Remagures.Model.PickUp
{
    public class PhysicsPickupable : MonoBehaviour, IPickupable
    {
        private IPickupable _pickupable;

        public void Construct(IPickupable pickupable)
            => _pickupable = pickupable ?? throw new ArgumentNullException(nameof(pickupable));

        public void Pickup()
            => _pickupable.Pickup();
    }
}