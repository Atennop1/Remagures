using Remagures.DialogSystem.View;

namespace Remagures.Cutscenes.Actions
{
    public class EndDialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        private readonly DialogTypeWriter _writer;

        public EndDialogAction(DialogTypeWriter writer) => _writer = writer;

        public void Start()
        {
            IsStarted = true;
            IsFinished = true;
        }

        public void Finish()
        {
            _writer.View.DialogWindow.SetActive(false);
        }
    }
}