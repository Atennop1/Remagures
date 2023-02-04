using UnityEngine;

namespace Remagures.SO
{
    [System.Serializable]
    public class LootData
    {
        [field: SerializeField] public GameObject LootObject { get; private set; }
        [field: SerializeField] public int LootChance { get; private set; }
    }
}