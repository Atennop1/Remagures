using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "MagicItem", menuName = "ItemsData/MagicItemData", order = 0)]
    public sealed class MagicItemData : ItemData
    {
        [field: SerializeField] public int UsingCooldownInMilliseconds { get; private set; }
        [field: SerializeField] public int AttackingTimeInMilliseconds { get; private set; }
    }
}