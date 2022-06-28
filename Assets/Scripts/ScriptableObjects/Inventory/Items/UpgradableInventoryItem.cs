using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableInventoryItem : UniqueInventoryItem
{
    [field: SerializeField] public UpgradableItemData UpgradableItemData { get; private set; } = new UpgradableItemData();
}
