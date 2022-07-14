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
        _magicSlot.Setup(_currentCell, this);
        
        IChoiceableItem choiceableItem = _currentCell.Item as IChoiceableItem;
        if (choiceableItem != null)
            choiceableItem.SetIsCurrent(PlayerInventory.MyInventory);

        _magicCounter.SetupProjectile((_currentCell.Item as IMagicItem)?.Projectile.GetComponent<Projectile>());
    }

    public override void SetButton() { }

    public override void SetupPlayer() { }
}
