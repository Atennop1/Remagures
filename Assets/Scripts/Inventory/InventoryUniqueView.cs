using UnityEngine;

public class InventoryUniqueView : MonoBehaviour
{
    [field: SerializeField] public InventoryView View { get; private set; }

    [field: SerializeField, Space] public PlayerInventory PlayerInventory { get; private set; }
    [field: SerializeField] public PlayerInventory MagicInventory { get; private set; }
    [field: SerializeField] public PlayerInventory RuneInventory { get; private set; }

    [Header("Unique Stuff")]
    [SerializeField] private InventorySlotUnique _helmetSlot;
    [SerializeField] private InventorySlotUnique _chestplateSlot;
    [SerializeField] private InventorySlotUnique _legginsSlot;

    [Space]
    [SerializeField] private InventorySlot _runeSlot;
    [field: SerializeField] public InventorySlotUnique WeaponSlot { get; private set; }
    [field: SerializeField] public InventorySlot MagicSlot { get; private set; }
    
    [field: SerializeField, Space] public BaseInventoryItem NoneItem { get; private set; }
    [field: SerializeField] public GameObject EquipButton { get; private set; }

    public void OnEnable()
    {
        EquipButton.SetActive(false);
    }
    
    public void SetUnique(PlayerController player)
    {
        float maxHelmetArmor = 0;
        float maxChestplateArmor = 0;
        float maxLegginsArmor = 0;
        int maxWeaponDamage = 0;

        _helmetSlot.Setup(NoneItem, View);
        _chestplateSlot.Setup(NoneItem, View);
        _legginsSlot.Setup(NoneItem, View);
        WeaponSlot.Setup(NoneItem, View);
        MagicSlot.Setup(NoneItem, View);
        _runeSlot.Setup(NoneItem, View);

        if (player != null)
        {
            player.PlayerAnimations.ChestplateAnimator.gameObject.SetActive(false);
            player.PlayerAnimations.LegginsAnimator.gameObject.SetActive(false);
            player.PlayerAnimations.HelmetAnimator.gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            ArmorInventoryItem armorItem = PlayerInventory.MyInventory[i] as ArmorInventoryItem;
            WeaponInventoryItem weaponItem = PlayerInventory.MyInventory[i] as WeaponInventoryItem;

            switch ((PlayerInventory.MyInventory[i] as UniqueInventoryItem).UniqueItemData.UniqueClass)
            {
                case UniqueClass.Helmet:
                    if (armorItem.ArmorItemData.Armor > maxHelmetArmor)
                    {
                        maxHelmetArmor = armorItem.ArmorItemData.Armor;
                        _helmetSlot.Setup(PlayerInventory.MyInventory[i], View);
                        if (player)
                        {
                            player.PlayerAnimations.HelmetAnimator.gameObject.SetActive(true);
                            player.PlayerAnimations.HelmetAnimator.runtimeAnimatorController = armorItem.DisplaybleItemData.OverrideController;
                        }
                    }
                    break;
                case UniqueClass.Chestplate:
                    if (armorItem.ArmorItemData.Armor > maxChestplateArmor)
                    {
                        maxChestplateArmor = armorItem.ArmorItemData.Armor;
                        _chestplateSlot.Setup(PlayerInventory.MyInventory[i], View);
                        if (player)
                        {
                            player.PlayerAnimations.ChestplateAnimator.gameObject.SetActive(true);
                            player.PlayerAnimations.ChestplateAnimator.runtimeAnimatorController = armorItem.DisplaybleItemData.OverrideController;
                        }
                    }
                    break;
                case UniqueClass.Leggins:
                    if (armorItem.ArmorItemData.Armor > maxLegginsArmor)
                    {
                        maxLegginsArmor = armorItem.ArmorItemData.Armor;
                        _legginsSlot.Setup(PlayerInventory.MyInventory[i], View);
                        if (player)
                        {
                            player.PlayerAnimations.LegginsAnimator.gameObject.SetActive(true);
                            player.PlayerAnimations.LegginsAnimator.runtimeAnimatorController = armorItem.DisplaybleItemData.OverrideController;
                        }
                    }
                    break;
                case UniqueClass.Weapon:
                    if (weaponItem.WeaponItemData.Damage > maxWeaponDamage)
                    {
                        maxWeaponDamage = weaponItem.WeaponItemData.Damage;
                        WeaponSlot.Setup(PlayerInventory.MyInventory[i], View);
                    }
                    break;
            }
        }
        
        for (int i = 0; i < MagicInventory.MyInventory.Count; i++)
        {
            MagicInventoryItem magicItem = MagicInventory.MyInventory[i] as MagicInventoryItem;
            if (magicItem != null && magicItem.MagicItemData.IsCurrent)
                MagicSlot.Setup(magicItem, View);
        }
        for (int i = 0; i < RuneInventory.MyInventory.Count; i++)
        {
            RuneInventoryItem runeItem = RuneInventory.MyInventory[i] as RuneInventoryItem;
            if (runeItem != null && runeItem.RuneItemData.IsCurrent)
                _runeSlot.Setup(runeItem, View);
        }  
    }
}
