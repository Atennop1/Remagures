using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New LootTable", menuName = "Other/LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private LootData[] LootObjects;

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