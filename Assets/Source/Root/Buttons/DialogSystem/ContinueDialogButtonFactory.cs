using Remagures.Model.Buttons;
using Remagures.Root.Dialogs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ContinueDialogButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private DialogPlayerFactory _dialogPlayerFactory;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new ContinueDialogButton(_dialogPlayerFactory.Create());
            return _builtButton;
        }
    }
}