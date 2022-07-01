using UnityEngine;

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
        int probility = 0;
        int currentProbility = Random.Range(0, 101);

        foreach (Loot loot in LootObjects)
        {
            probility += loot.LootChance;
            if (currentProbility <= probility)
            {
                return loot.LootObject;
            }
        }
        return null;
    }
}
