using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Pickup
{
    public sealed class PhysicsPickupable : SerializedMonoBehaviour, IPickupable
    {
        private IPickupable _pickupable;

        public void Construct(IPickupable pickupable)
            => _pickupable = pickupable ?? throw new ArgumentNullException(nameof(pickupable));

        public void Pickup()
            => _pickupable.Pickup();
    }
}