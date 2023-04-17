using System.Collections.Generic;
using System.Linq;
using Remagures.Model.Health;
using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorFactory : MonoBehaviour, IArmorFactory 
    {
        private Armor _builtArmor;

        IArmor IArmorFactory.Create()
            => Create();
        
        public Armor Create()
        {
            if (_builtArmor != null)
                return _builtArmor;

            _builtArmor = new Armor(0);
            return _builtArmor;
        }
    }
}