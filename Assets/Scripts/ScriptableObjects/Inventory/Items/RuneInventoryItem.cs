using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Items/RuneItem", fileName="New Rune")]
public class RuneInventoryItem : UniqueInventoryItem
{
    [field: SerializeField] public RuneItemData RuneItemData { get; private set; } = new RuneItemData();
}
