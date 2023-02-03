namespace Remagures.DialogSystem
{
    public interface IDialogsList
    {
        public Dialog CurrentDialog { get; }
        public void SwitchToDialog(string dialogName);
        public void SwitchToDialog(Dialog dialog);
    }
}