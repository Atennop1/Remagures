using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public class UniqueEntryPoint : MonoBehaviour //TODO delete this class after making root layer
    {
        [SerializeField] private CharacterHealth _characterHealth;
        [SerializeField] private Player _player;
        
        [Space]
        [SerializeField] private UniqueSetup _uniqueSetup;
        [SerializeField] private MagicCounter _magicCounter;
        
        public void UpdateArmor()
        {
            var rawTotalArmor = _uniqueSetup.UniqueInventory.Cells.Select(cell => cell.Item).OfType<IArmorItem>().Sum(armorItem => armorItem.Armor);
            _characterHealth.SetTotalArmor(Mathf.RoundToInt(rawTotalArmor));
        }
        
        private void Awake()
        {
            UpdateArmor();
            _uniqueSetup.SetupUnique(_player);
            
            if (_uniqueSetup.MagicCellView != null && _uniqueSetup.MagicCellView.ThisCell.Item is IMagicItem magicItem && magicItem.Projectile != null)
                _magicCounter.SetupProjectile(magicItem.Projectile);
        }
    }
}