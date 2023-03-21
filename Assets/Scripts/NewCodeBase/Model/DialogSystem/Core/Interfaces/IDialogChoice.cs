namespace Remagures.Model.DialogSystem
{
    public interface IDialogChoice
    {
        string Text { get; }
        
        bool IsUsed { get; }
        void Use();
    }
}