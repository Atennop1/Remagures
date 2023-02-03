using UnityEngine;

namespace Remagures.SO
{
    [System.Serializable]
    public class Loot
    {
        [field: SerializeField] public GameObject LootObject { get; private set; }
        [field: SerializeField] public int LootChance { get; private set; }
    }

    [CreateAssetMenu(fileName = "New LootTable", menuName = "Other/LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private Loot[] LootObjects;

        public GameObject Loot()
        {
            var probability = 0;
            var currentProbability = Random.Range(0, 101);

            foreach (var loot in LootObjects)
            {
                probability += loot.LootChance;
                if (currentProbability <= probability)
                    return loot.LootObject;
            }
        
            return null;
        }
    }
}