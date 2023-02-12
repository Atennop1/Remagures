using System;
using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;

namespace Remagures.Model.Pickup
{
    public class UniqueItemPickupable : IPickupable
    {
        private readonly IPickupable _pickupable;
        private readonly UniqueEntryPoint _uniqueEntryPoint;

        public UniqueItemPickupable(IPickupable pickupable, UniqueEntryPoint uniqueEntryPoint)
        {
            _pickupable = pickupable ?? throw new ArgumentNullException(nameof(pickupable));
            _uniqueEntryPoint = uniqueEntryPoint ?? throw new ArgumentNullException(nameof(uniqueEntryPoint));
        }

        public void Pickup()
        {
            _pickupable.Pickup();
            _uniqueEntryPoint.UpdateArmor();
        }
    }
}