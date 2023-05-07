using System.Collections.Generic;
using System.Linq;
using Remagures.Model.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ButtonsFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private List<IButtonFactory> _buttonFactories;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new Buttons(_buttonFactories.Select(factory => factory.Create()).ToList());
            return _builtButton;
        }
    }
}