using System;
using Remagures.Model.DialogSystem;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class DialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished => _dialogPlayer.HasPlayed;

        private readonly DialogPlayer _dialogPlayer;
        private readonly Dialog _dialog;

        public DialogAction(DialogPlayer dialogPlayer, Dialog dialog)
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