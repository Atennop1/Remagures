using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayableInventoryItem : UpgradableInventoryItem
{
    [field: SerializeField] public DisplayableItemData DisplaybleItemData { get; private set; } = new DisplayableItemData();
}
