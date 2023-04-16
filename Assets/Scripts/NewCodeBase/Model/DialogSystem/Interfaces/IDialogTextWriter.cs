using Cysharp.Threading.Tasks;

namespace Remagures.Model.DialogSystem
{
    public interface IDialogTextWriter
    {
        bool IsTyping { get; }
        UniTask StartTyping(string text);
        void EndTyping();
    }
}