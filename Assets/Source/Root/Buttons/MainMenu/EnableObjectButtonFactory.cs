using Remagures.Model.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class EnableObjectButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private GameObject _gameObject;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new EnableObjectButton(_gameObject);
            return _builtButton;
        }
    }
}