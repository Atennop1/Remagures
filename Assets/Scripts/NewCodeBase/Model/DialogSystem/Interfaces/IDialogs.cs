namespace Remagures.Model.DialogSystem
{
    public interface IDialogs
    {
        IDialog CurrentDialog { get; }
        void SwitchCurrent(string dialogName);
    }
}