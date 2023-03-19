using System;
using Remagures.Model.Health;
using Remagures.Model.InventorySystem;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.Pickup
{
    public class UniqueItemPickupable : IPickupable
    {
        private readonly IPickupable _pickupable;
        private readonly IArmorFactory _armorFactory;

        public UniqueItemPickupable(IPickupable pickupable, ArmorFactory armorFactory)
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