using System;
using Remagures.Root;

namespace Remagures.Model.Pickup
{
    public sealed class PickupableWithArmorSetup : IPickupable
    {
        private readonly IPickupable _pickupable;
        private readonly IArmorSetuper _armorSetuper;

        public PickupableWithArmorSetup(IPickupable pickupable, IArmorSetuper armorSetuper)
        {
            _pickupable = pickupable ?? throw new ArgumentNullException(nameof(pickupable));
            _armorSetuper = armorSetuper ?? throw new ArgumentNullException(nameof(armorSetuper));
        }

        public void Pickup()
        {
            _pickupable.Pickup();
            _armorSetuper.Setup();
        }
    }
}