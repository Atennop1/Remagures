using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class CurrentDialogPlaying : IAdditionalBehaviour
    {
        private readonly IDialogs _dialogs;
        private readonly DialogPlayer _dialogPlayer;

        public CurrentDialogPlaying(DialogPlayer dialogPlayer, IDialogs dialogs)
        {
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
        }
        
        public void Use()
            => _dialogPlayer.Play(_dialogs.CurrentDialog);
    }
}