using Remagures.Player;
using Remagures.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.Inventory
{
    public class UniqueSetup : MonoBehaviour
    {
        [FormerlySerializedAs("View")] [SerializeField] private InventoryView _view;

        [field: SerializeField, Space] public PlayerInventory UniqueInventory { get; private set; }
        [FormerlySerializedAs("MagicInventory")] [SerializeField] private PlayerInventory _magicInventory;
        [FormerlySerializedAs("RuneInventory")] [SerializeField] private PlayerInventory _runeInventory;

        [Space]
        [SerializeField] private Slot _helmetSlot;
        [SerializeField] private Slot _chestplateSlot;
        [SerializeField] private Slot _legginsSlot;

        [Space]
        [SerializeField] private Slot _runeSlot;
        [field: SerializeField] public Slot WeaponSlot { get; private set; }
        [field: SerializeField] public Slot MagicSlot { get; private set; }

        [Space]
        [FormerlySerializedAs("NoneItem")] [SerializeField] private BaseInventoryItem _noneItem;
        [FormerlySerializedAs("EquipButton")] [SerializeField] private GameObject _equipButton;

        private PlayerAnimations _playerAnimations;
        private PlayerMovement _playerMovement;

        public void SetupUnique(Player.Player player)
        {
            _playerAnimations = player.GetPlayerComponent<PlayerAnimations>();
            _playerMovement = player.GetPlayerComponent<PlayerMovement>();
                
            ClearPlayerUnique(player);
            
            float currentMaxHelmetArmor = 0;
            float currentMaxChestplateArmor = 0;
            float currentMaxLegginsArmor = 0;
            float currentMaxWeaponDamage = 0;

            foreach (var cell in UniqueInventory.MyInventory)
            {
                switch ((cell.Item as IUniqueItem)?.UniqueType)
                {
                    case UniqueType.Helmet:
                        SetArmorPart(cell, _helmetSlot, ref currentMaxHelmetArmor, _playerAnimations.HelmetAnimator);
                        break;

                    case UniqueType.Chestplate:
                        SetArmorPart(cell, _chestplateSlot, ref currentMaxChestplateArmor, _playerAnimations.ChestplateAnimator);
                        break;

                    case UniqueType.Leggins:
                        SetArmorPart(cell, _legginsSlot, ref currentMaxLegginsArmor, _playerAnimations.LegginsAnimator);
                        break;

                    case UniqueType.Weapon:
                        var weaponItem = cell.Item as IWeaponItem;
                        if (weaponItem?.Damage > currentMaxWeaponDamage)
                        {
                            currentMaxWeaponDamage = weaponItem.Damage;
                            WeaponSlot.Setup(cell, _view);
                        }
                        break;
                
                    case null:
                        break;
                }
            }
        
            foreach (var cell in _magicInventory.MyInventory)
                SetupChoiceable(MagicSlot, cell);

            foreach (var cell in _runeInventory.MyInventory)
                SetupChoiceable(_runeSlot, cell);
            
            _playerAnimations.SetAnimFloat(_playerMovement.PlayerViewDirection);
        }

        private void ClearPlayerUnique(Player.Player player)
        {
            var nullCell = new Cell(_noneItem);

            _helmetSlot.Setup(nullCell, _view);
            _chestplateSlot.Setup(nullCell, _view);
            _legginsSlot.Setup(nullCell, _view);

            WeaponSlot.Setup(nullCell, _view);
            MagicSlot.Setup(nullCell, _view);
            _runeSlot.Setup(nullCell, _view);

            if (player == null) return;
            
            _playerAnimations.ChestplateAnimator.gameObject.SetActive(false);
            _playerAnimations.LegginsAnimator.gameObject.SetActive(false);
            _playerAnimations.HelmetAnimator.gameObject.SetActive(false);
        }

        private void SetupChoiceable(Slot slot, IReadOnlyCell cell)
        {
            if (cell.Item is IChoiceableItem { IsCurrent: true } choiceableItem)
                slot.Setup(new Cell(choiceableItem as BaseInventoryItem), _view);
        }

        private void SetArmorPart(IReadOnlyCell cell, Slot slot, ref float currentMaxValue, Animator animator = null)
        {
            var armorItem = cell.Item as IArmorItem;
            if (armorItem?.Armor <= currentMaxValue) return;

            if (armorItem != null) currentMaxValue = armorItem.Armor;
            var displayableItem = cell.Item as IDisplayableItem;

            if (animator != null)
            {
                animator.gameObject.SetActive(true);
                animator.runtimeAnimatorController = displayableItem?.OverrideController;
            }

            slot.Setup(cell, _view);
        }
        
        private void OnEnable()
        {
            _equipButton.SetActive(false);
        }
    }
}
