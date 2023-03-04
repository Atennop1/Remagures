using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "ArmorItemData", menuName = "ItemsData/ArmorItem")]
    public sealed class ArmorItemData : ItemData
    {
        [field: SerializeField] public AnimatorOverrideController AnimatorController { get; private set; }
        [field: SerializeField] public int Armor { get; private set; }
    }
}