using Remagures.Model.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class OpenParentMapButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private ParentMapOpenerFactory _parentMapOpenerFactory;
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