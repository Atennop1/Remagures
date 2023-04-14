using Remagures.Model.DialogSystem;
using Remagures.View.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogPlayerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private DialogTextWriterFactory _dialogTextWriterFactory;
        [SerializeField] private DialogView _dialogView;
        private DialogPlayer _builtPlayer;

        public DialogPlayer Create()
        {
            if (_builtPlayer != null)
                return _builtPlayer;

            _builtPlayer = new DialogPlayer(_dialogTextWriterFactory.Create(), _dialogView);
            return _builtPlayer;
        }
    }
}