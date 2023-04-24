using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Factories
{
    [CreateAssetMenu(fileName = "Loot Prefabs", menuName = "Loot/Prefabs", order = 0)]
    public sealed class Loots : ScriptableObject
    {
        [field: SerializeField] public List<Loot> List { get; private set; }
    }
}