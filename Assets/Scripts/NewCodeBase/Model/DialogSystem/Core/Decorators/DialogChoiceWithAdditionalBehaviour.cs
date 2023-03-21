namespace Remagures.Model.DialogSystem
{
    public sealed class DialogChoiceWithAdditionalBehaviour : IDialogChoice
    {
        public string Text => _dialogChoice.Text;
        public bool IsUsed => _dialogChoice.IsUsed;

        private readonly IDialogChoice _dialogChoice;
        private readonly IAdditionalBehaviour _additionalBehaviour;
        
        public void Use()
        {
            _additionalBehaviour.Use();
            _dialogChoice.Use();
        }
    }
}