namespace Remagures.Model.DialogSystem
{
    public interface IDialogPlayer
    {
        bool HasPlayed { get; }
        void Play(IDialog dialog);
        void ContinueDialog();
    }
}