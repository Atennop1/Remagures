using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.PickUp
{
    public class UniqueItemPickupable : IPickupable
    {
        private readonly IPickupable _pickupable;
        private readonly UniqueSetup _uniqueSetup;
        private readonly UniqueEntryPoint _uniqueEntryPoint;

        public UniqueItemPickupable(IPickupable pickupable, UniqueSetup uniqueSetup, UniqueEntryPoint uniqueEntryPoint)
        {
            _pickupable = pickupable ?? throw new ArgumentNullException(nameof(pickupable));
            _uniqueSetup = uniqueSetup ?? throw new ArgumentNullException(nameof(uniqueSetup));
            _uniqueEntryPoint = uniqueEntryPoint ?? throw new ArgumentNullException(nameof(uniqueEntryPoint));
        }

        public void Pickup()
        {
            _pickupable.Pickup();
            _uniqueEntryPoint.UpdateArmor();
            _uniqueSetup.SetupUnique(player);
        }
    }
}