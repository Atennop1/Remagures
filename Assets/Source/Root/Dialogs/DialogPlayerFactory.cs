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
        
        private DialogPlayer _builtPlayer;
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();

        public IDialogPlayer Create()
        {
            if (_builtPlayer != null)
                return _builtPlayer;

            _builtPlayer = new DialogPlayer(_dialogTextWriterFactory.Create(), _dialogView);
            _lateSystemUpdate.Add(_builtPlayer);
            return _builtPlayer;
        }
    }
}