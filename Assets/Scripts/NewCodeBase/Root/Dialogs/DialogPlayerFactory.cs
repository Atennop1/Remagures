using Remagures.Model.DialogSystem;
using Remagures.View.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class DialogPlayerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private DialogTextWriterFactory _dialogTextWriterFactory;
        [SerializeField] private DialogView _dialogView;
        private IDialogPlayer _builtPlayer;

        public IDialogPlayer Create()
        {
            if (_builtPlayer != null)
                return _builtPlayer;

            _builtPlayer = new DialogPlayer(_dialogTextWriterFactory.Create(), _dialogView);
            return _builtPlayer;
        }
    }
}