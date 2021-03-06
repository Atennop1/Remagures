using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/ArmorItem")]
public class ArmorInventoryItem : BaseInventoryItem, IArmorItem, IUniqueItem, IDisplayableItem, IUpgradableItem
{
    
    [field: SerializeField, Header("Armor Stuff")] public UniqueClass UniqueClass { get; private set; }
    [field: SerializeField] public AnimatorOverrideController OverrideController { get; private set; }
    [field: SerializeField] public float Armor { get; private set; }
    
    [field: SerializeField, Header("Upgradable Stuff")] public int ThisItemLevel { get; private set; }
    [field: SerializeField] public int CostForThisItem { get; private set; }
    [field: SerializeField] public List<BaseInventoryItem> ItemsForLevels { get; private set; }
}
