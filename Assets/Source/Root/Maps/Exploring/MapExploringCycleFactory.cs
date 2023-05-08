using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapExploringCycleFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IMapExplorerFactory _mapExplorerFactory;

        public void Awake()
        {
            var cycle = new MapExploringCycle(_mapExplorerFactory.Create());
            cycle.Activate();
        }
    }
}