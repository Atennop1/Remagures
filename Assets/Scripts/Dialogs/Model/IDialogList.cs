namespace Remagures.Dialogs.Model
{
    public interface IDialogsList<T>
    {
        public Dialog CurrentDialog { get; }
        public void SwitchToDialog(string dialogName);
        public void SwitchToDialog(Dialog dialog);
    }
}