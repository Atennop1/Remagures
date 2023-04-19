using System;
using UnityEngine;

namespace Remagures.Factories
{
    [Serializable]
    public sealed class Loot
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int Chance { get; private set; }
    }
}