using System;
using Remagures.Root;

namespace Remagures.Model.Pickup
{
    public class PickupableWithArmorSetup : IPickupable
    {
        private readonly IPickupable _pickupable;
        private readonly IArmorFactory _armorFactory;

        public PickupableWithArmorSetup(IPickupable pickupable, IArmorFactory armorFactory)
        {
            _pickupable = pickupable ?? throw new ArgumentNullException(nameof(pickupable));
            _armorFactory = armorFactory ?? throw new ArgumentNullException(nameof(armorFactory));
        }

        public void Pickup()
        {
            _pickupable.Pickup();
            _armorFactory.Create();
        }
    }
}