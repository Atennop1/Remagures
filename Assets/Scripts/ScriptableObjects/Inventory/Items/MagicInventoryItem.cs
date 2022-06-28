using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/MagicItem")]
public class MagicInventoryItem : UniqueInventoryItem
{
    [field: SerializeField] public MagicItemData MagicItemData { get; private set; } = new MagicItemData();
    [field: SerializeField] public WeaponItemData WeaponItemData { get; private set; } = new WeaponItemData();
}
