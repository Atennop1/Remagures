using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class LootFactory : SerializedMonoBehaviour, ILootFactory
    {
        [SerializeField] private Loots _loots;
        
        public void CreateRandom(Vector3 position)
        {
            var accumulatedProbability = 0;
            var probability = Random.Range(0, 101);

            foreach (var loot in _loots.List)
            {
                accumulatedProbability += loot.Chance;

                if (accumulatedProbability > probability) 
                    continue;
                
                Instantiate(loot.Prefab, position, Quaternion.identity);
                return;
            }
        }
    }
}