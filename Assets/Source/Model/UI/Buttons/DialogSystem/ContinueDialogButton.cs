using Remagures.Model.DialogSystem;

namespace Remagures.Model.UI
{
    public sealed class ContinueDialogButton : IButton
    {
        private readonly IDialogPlayer _dialogPlayer;
        
        public void Press() 
            => _dialogPlayer.ContinueDialog();
    }
}