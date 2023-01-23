using Remagures.DialogSystem.View;

namespace Remagures.Cutscenes.Actions
{
    public class EndDialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        
        private readonly DialogView _dialogView;

        public EndDialogAction(DialogView dialogView) 
            => _dialogView = dialogView;

        public void Start()
        {
            IsStarted = true;
            IsFinished = true;
        }

        public void Finish()
            => _dialogView.gameObject.SetActive(false);
    }
}