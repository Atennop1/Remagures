using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorSetuper : SerializedMonoBehaviour, IArmorSetuper
    {
        [SerializeField] private List<IInventorySelectorFactory<IArmorItem>> _armorSelectorsFactories;
        [SerializeField] private ArmorFactory _armorFactory;
        
        public void Setup()
        {
            var armor = _armorSelectorsFactories.Select(factory => factory.Create()).Sum(selector => selector.SelectedCell.Item.Armor);
            _armorFactory.Create().SetValue(armor);
        }

        private void Awake()
            => Setup();
    }
}