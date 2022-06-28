using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/WeaponItem")]
public class WeaponInventoryItem : DisplayableInventoryItem
{
    [field: SerializeField] public WeaponItemData WeaponItemData { get; private set; } = new WeaponItemData();
}
