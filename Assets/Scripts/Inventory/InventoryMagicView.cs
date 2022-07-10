using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMagicView : InventoryView
{
    [Header("Magic Stuff")]
    [SerializeField] private MagicCounter _magicCounter;
    [SerializeField] private InventorySlot _magicSlot;

    public void Equip()
    {
        _magicSlot.Setup(_currentItem, this);
        
        MagicInventoryItem magicItem = _currentItem as MagicInventoryItem;
        if (magicItem != null)
            magicItem.MagicItemData.SetIsCurrent(PlayerInventory.MyInventory);

        _magicCounter.SetupProjectile((_currentItem as MagicInventoryItem).MagicItemData.Projectile.GetComponent<Projectile>());
    }

    public override void SetButton() { }

    public override void SetupPlayer() { }
}
