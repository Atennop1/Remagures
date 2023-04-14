using System.Collections.Generic;
using System.Linq;
using Remagures.Model.Health;
using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorFactory : MonoBehaviour, IArmorFactory //TODO separate this to factory and setuper
    {
        [SerializeField] private List<IInventorySelectorFactory<IArmorItem>> _armorSelectorsFactories;
        private Armor _builtArmor;

        IArmor IArmorFactory.Create()
            => Create();
        
        private Armor Create()
        {
            var armor = _armorSelectorsFactories.Select(factory => factory.Create()).Sum(selector => selector.SelectedCell.Item.Armor);

            if (_builtArmor != null)
            {
                _builtArmor.SetValue(armor);
                return _builtArmor;
            }
            
            _builtArmor = new Armor(armor);
            return _builtArmor;
        }
    }
}