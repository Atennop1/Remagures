using Remagures.Model.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class OpenParentMapButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private IParentMapOpenerFactory _parentMapOpenerFactory;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new OpenParentMapButton(_parentMapOpenerFactory.Create());
            return _builtButton;
        }
    }
}