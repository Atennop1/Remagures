using System;
using Remagures.Model.DialogSystem;
using Remagures.View.DialogSystem;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class CloseDialogWindowActionDialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        
        private readonly DialogView _dialogView;

        public CloseDialogWindowActionDialogAction(DialogView dialogView) 
            => _dialogView = dialogView ? dialogView : throw new ArgumentNullException(nameof(dialogView));

        public void Start()
        {
            IsStarted = true;
            IsFinished = true;
        }

        public void Finish()
            => _dialogView.gameObject.SetActive(false);
    }
}