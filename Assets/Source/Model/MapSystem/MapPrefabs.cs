using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class MapPrefabs
    {
        private readonly IReadOnlyDictionary<IMap, GameObject> _prefabs;

        public MapPrefabs(Dictionary<IMap, GameObject> prefabs)
            => _prefabs = prefabs ?? throw new ArgumentNullException(nameof(prefabs));

        public GameObject GetFor(IMap map)
            => _prefabs[map];
    }
}