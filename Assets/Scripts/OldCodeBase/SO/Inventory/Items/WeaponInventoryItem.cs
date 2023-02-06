using Remagures.Inventory;
using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/WeaponItem")]
    public class WeaponInventoryItem : BaseInventoryItem, IWeaponItem, IDisplayableItem, IUniqueItem
    {
        [field: SerializeField, Header("Weapon Stuff")] public UniqueType UniqueType { get; private set; }
        [field: SerializeField] public AnimatorOverrideController OverrideController { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
