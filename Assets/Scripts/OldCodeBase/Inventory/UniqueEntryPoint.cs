using System.Linq;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Inventory
{
    public class UniqueEntryPoint : MonoBehaviour
    {
        [SerializeField] private CharacterHealth _characterHealth;
        [SerializeField] private Player _player;
        
        [Space]
        [SerializeField] private UniqueSetup _uniqueSetup;
        [SerializeField] private MagicCounter _magicCounter;
        
        public void UpdateArmor()
        {
            var rawTotalArmor = _uniqueSetup.UniqueInventory.MyInventory.Select(cell => cell.Item).OfType<IArmorItem>().Sum(armorItem => armorItem.Armor);
            _characterHealth.SetTotalArmor(Mathf.RoundToInt(rawTotalArmor));
        }
        
        private void Awake()
        {
            UpdateArmor();
            _uniqueSetup.SetupUnique(_player);
            
            if (_uniqueSetup.MagicSlot != null && _uniqueSetup.MagicSlot.ThisCell.Item is IMagicItem magicItem && magicItem.Projectile != null)
                _magicCounter.SetupProjectile(magicItem.Projectile);
        }
    }
}