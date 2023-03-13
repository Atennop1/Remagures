﻿using System.Collections.Generic;
using System.Linq;
using Remagures.Model.Health;
using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorFactory : MonoBehaviour, IArmorFactory
    {
        [SerializeField] private List<IInventorySelectorFactory<IArmorItem>> _armorSelectorsFactories;
        private Armor _builtArmor;

        IArmor IArmorFactory.Create()
            => Create();
        
        public Armor Create()
        {
            var armor = _armorSelectorsFactories.Select(factory => factory.Create()).Sum(selector => selector.SelectedCell.Item.Armor);

            if (_builtArmor != null)
            {
                _builtArmor.SetArmor(armor);
                return _builtArmor;
            }
            
            _builtArmor = new Armor(armor);
            return _builtArmor;
        }
        
    }
}