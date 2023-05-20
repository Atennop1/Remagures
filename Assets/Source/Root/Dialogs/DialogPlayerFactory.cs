using Remagures.Model.DialogSystem;
using Remagures.View.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class DialogPlayerFactory : SerializedMonoBehaviour, IDialogPlayerFactory
    {
        [SerializeField] private IDialogTextWriterFactory _dialogTextWriterFactory;
        [SerializeField] private IDialogView _dialogView;
        
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