using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicUpgradableItem : PhysicInventoryItem
{
    protected override bool CanAddItem()
    {
        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
            for (int j = 0; j < (PlayerInventory.MyInventory[i].Item as IUpgradableItem)?.ItemsForLevels.Count; j++)
                if ((PlayerInventory.MyInventory[i].Item as IUpgradableItem)?.ItemsForLevels[j] == ThisItem)
                    return false;
        
        return true;
    }
}
