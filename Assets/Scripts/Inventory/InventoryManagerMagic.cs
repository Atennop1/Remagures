using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerMagic : InventoryManager
{
    [Header("Magic Stuff")]
    [SerializeField] private MagicManager _magicManager;
    [SerializeField] private InventorySlot _magicSlot;

    public void Equip()
    {
        _magicSlot.Setup(_currentItem, this);
        
        MagicInventoryItem magicItem = _currentItem as MagicInventoryItem;
        if (magicItem != null)
            magicItem.MagicItemData.SetIsCurrent(PlayerInventory.MyInventory);

        _magicManager.SetupProjectile((_currentItem as MagicInventoryItem).MagicItemData.Projectile.GetComponent<Projectile>());
    }

    public override void SetButton() { }

    public override void SetupPlayer() { }
}
