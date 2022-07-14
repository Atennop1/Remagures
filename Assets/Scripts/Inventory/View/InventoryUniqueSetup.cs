using UnityEngine;

public class InventoryUniqueSetup : MonoBehaviour
{
    [field: SerializeField] public InventoryView View { get; private set; }

    [field: SerializeField, Space] public PlayerInventory PlayerInventory { get; private set; }
    [field: SerializeField] public PlayerInventory MagicInventory { get; private set; }
    [field: SerializeField] public PlayerInventory RuneInventory { get; private set; }

    [Header("Unique Stuff")]
    [SerializeField] private InventorySlot _helmetSlot;
    [SerializeField] private InventorySlot _chestplateSlot;
    [SerializeField] private InventorySlot _legginsSlot;

    [Space]
    [SerializeField] private InventorySlot _runeSlot;
    [field: SerializeField] public InventorySlot WeaponSlot { get; private set; }
    [field: SerializeField] public InventorySlot MagicSlot { get; private set; }
    
    [field: SerializeField, Space] public BaseInventoryItem NoneItem { get; private set; }
    [field: SerializeField] public GameObject EquipButton { get; private set; }

    public void OnEnable()
    {
        EquipButton.SetActive(false);
    }
    
    public void SetUnique(Player player)
    {
        float currentMaxHelmetArmor = 0;
        float currentMaxChestplateArmor = 0;
        float currentMaxLegginsArmor = 0;
        float currentMaxWeaponDamage = 0;

        _helmetSlot.Setup(new Cell(NoneItem), View);
        _chestplateSlot.Setup(new Cell(NoneItem), View);
        _legginsSlot.Setup(new Cell(NoneItem), View);

        WeaponSlot.Setup(new Cell(NoneItem), View);
        MagicSlot.Setup(new Cell(NoneItem), View);
        _runeSlot.Setup(new Cell(NoneItem), View);

        if (player != null)
        {
            player.PlayerAnimations.ChestplateAnimator.gameObject.SetActive(false);
            player.PlayerAnimations.LegginsAnimator.gameObject.SetActive(false);
            player.PlayerAnimations.HelmetAnimator.gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            switch ((PlayerInventory.MyInventory[i].Item as IUniqueItem)?.UniqueClass)
            {
                case UniqueClass.Helmet:
                    SetArmorPath(PlayerInventory.MyInventory[i], _helmetSlot, ref currentMaxHelmetArmor, player.PlayerAnimations.HelmetAnimator);
                    break;

                case UniqueClass.Chestplate:
                    SetArmorPath(PlayerInventory.MyInventory[i], _chestplateSlot, ref currentMaxChestplateArmor, player.PlayerAnimations.ChestplateAnimator);
                    break;

                case UniqueClass.Leggins:
                    SetArmorPath(PlayerInventory.MyInventory[i], _legginsSlot, ref currentMaxLegginsArmor, player.PlayerAnimations.LegginsAnimator);
                    break;

                case UniqueClass.Weapon:
                    IWeaponItem weaponItem = PlayerInventory.MyInventory[i].Item as IWeaponItem;
                    if (weaponItem?.Damage > currentMaxWeaponDamage)
                    {
                        currentMaxWeaponDamage = weaponItem.Damage;
                        WeaponSlot.Setup(PlayerInventory.MyInventory[i], View);
                    }
                    break;
            }
        }
        
        for (int i = 0; i < MagicInventory.MyInventory.Count; i++)
            SetupChoiceable(MagicSlot, MagicInventory.MyInventory[i]);

        for (int i = 0; i < RuneInventory.MyInventory.Count; i++)
            SetupChoiceable(_runeSlot, RuneInventory.MyInventory[i]);
    }

    public void SetupChoiceable(InventorySlot slot, IReadOnlyCell cell)
    {
        IChoiceableItem choiceableItem = cell.Item as IChoiceableItem;
            if (choiceableItem != null && choiceableItem.IsCurrent)
                slot.Setup(new Cell(choiceableItem as BaseInventoryItem, 1), View);
    }

    public void SetArmorPath(IReadOnlyCell cell, InventorySlot slot, ref float currentMaxValue, Animator animator = null)
    {
        IArmorItem armorItem = cell.Item as IArmorItem;

        if (armorItem?.Armor > currentMaxValue)
            currentMaxValue = armorItem.Armor;

        if (animator != null)
        {
            IDisplayableItem displayableItem = cell.Item as IDisplayableItem;
            animator.gameObject.SetActive(true);
            animator.runtimeAnimatorController = displayableItem?.OverrideController;
        }

        slot.Setup(cell, View);
    }
}
