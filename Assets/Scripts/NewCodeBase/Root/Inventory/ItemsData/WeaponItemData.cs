using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "ItemsData/WeaponItemData", order = 0)]
    public sealed class WeaponItemData : ItemData
    {
        [field: SerializeField] public AnimatorOverrideController AnimatorController { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}