using System;
using Remagures.Model.DialogSystem;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class DialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished => _dialogPlayer.HasPlayed;

        private readonly IDialogPlayer _dialogPlayer;
        private readonly IDialog _dialog;

        public DialogAction(IDialogPlayer dialogPlayer, IDialog dialog)
        {
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _dialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
        }

        public void Start()
        {
            IsStarted = true;
            _dialogPlayer.Play(_dialog);
        }
        
        public void Finish() { }
    }
}