using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradableItemData
{
    [field: SerializeField, Header("Upgradable Stuff")] public int ThisItemLevel { get; private set; }
    [field: SerializeField] public int CostForThisItem { get; private set; }
    [field: SerializeField] public List<BaseInventoryItem> ItemsForLevels { get; private set; }
}
