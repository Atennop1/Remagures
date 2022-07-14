using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/WeaponItem")]
public class WeaponInventoryItem : BaseInventoryItem, IWeaponItem, IDisplayableItem, IUniqueItem
{
    [field: SerializeField, Header("Weapon Stuff")] public UniqueClass UniqueClass { get; private set; }
    [field: SerializeField] public AnimatorOverrideController OverrideController { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}
