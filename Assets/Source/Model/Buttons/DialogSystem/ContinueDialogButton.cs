using System;
using Remagures.Model.DialogSystem;

namespace Remagures.Model.Buttons
{
    public sealed class ContinueDialogButton : IButton
    {
        private readonly IDialogPlayer _dialogPlayer;

        public ContinueDialogButton(IDialogPlayer dialogPlayer) 
            => _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));

        public void Press() 
            => _dialogPlayer.ContinueDialog();
    }
}