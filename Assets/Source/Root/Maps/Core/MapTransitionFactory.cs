using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapTransitionFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IMapFactory _mapFactory;
        private IMapTransition _builtTransition;
        
        public IMapTransition Create()
        {
            if (_builtTransition != null)
                return _builtTransition;

            _builtTransition = new MapTransition(_mapFactory.Create());
            return _builtTransition;
        }
    }
}