using System.Linq;
using Remagures.Player;
using UnityEngine;

namespace Remagures.Inventory
{
    public class UniqueEntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Player.Player _player;
        
        [Space]
        [SerializeField] private UniqueSetup _uniqueSetup;
        [SerializeField] private MagicCounter _magicCounter;
        
        public void UpdateArmor()
        {
            var rawTotalArmor = _uniqueSetup.UniqueInventory.MyInventory.Select(cell => cell.Item).OfType<IArmorItem>().Sum(armorItem => armorItem.Armor);
            _playerHealth.SetTotalArmor(Mathf.RoundToInt(rawTotalArmor));
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