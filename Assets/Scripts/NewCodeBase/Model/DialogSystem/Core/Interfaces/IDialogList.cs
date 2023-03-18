namespace Remagures.Model.DialogSystem
{
    public interface IDialogs
    {
        Dialog CurrentDialog { get; }
        void SwitchCurrent(string dialogName);
    }
}