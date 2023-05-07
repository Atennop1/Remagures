using Remagures.Model.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapTransitionButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private MapTransitionFactory _mapTransitionFactory;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new MapTransitionButton(_mapTransitionFactory.Create());
            return _builtButton;
        }
    }
}