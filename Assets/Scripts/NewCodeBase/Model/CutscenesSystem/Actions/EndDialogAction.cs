﻿using System;
using Remagures.Model.DialogSystem;
using Remagures.View.DialogSystem;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class EndDialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        
        private readonly DialogView _dialogView;

        public EndDialogAction(DialogView dialogView) 
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