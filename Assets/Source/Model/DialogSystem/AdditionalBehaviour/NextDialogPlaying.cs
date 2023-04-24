using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class NextDialogPlaying : IAdditionalBehaviour
    {
        private readonly IDialogs _dialogs;
        private readonly IDialogPlayer _dialogPlayer;

        public NextDialogPlaying(IDialogPlayer dialogPlayer, IDialogs dialogs)
        {
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
        }
        
        public void Use()
            => _dialogPlayer.Play(_dialogs.CurrentDialog);
    }
}