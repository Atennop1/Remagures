using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapExploringCycleFactory : SerializedMonoBehaviour
    {
        [SerializeField] private MapExplorerFactory _mapExplorerFactory;
        private MapExploringCycle _builtCycle;
        
        public MapExploringCycle Create()
        {
            if (_builtCycle != null)
                return _builtCycle;

            _builtCycle = new MapExploringCycle(_mapExplorerFactory.Create());
            return _builtCycle;
        }
    }
}