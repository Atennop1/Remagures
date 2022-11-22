using Remagures.DialogSystem.UI;
using UnityEngine.UI;

namespace Remagures.Cutscenes.Actions
{
    public class DialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set;  }

        private readonly DialogTypeWriter _writer;
        private readonly string _text;
        
        public DialogAction(DialogTypeWriter writer, string text)
        {
            _writer = writer;
            _text = text;
        }
        
        public async void Start()
        {
            IsStarted = true;
            _writer.View.DialogWindow.SetActive(true);
            
            _writer.View.DialogWindow.GetComponent<Button>().onClick.AddListener(Tap);
            await _writer.Type(_text);
            _writer.View.ContinueText.text = "Нажмите, чтобы продолжить";
        }

        private void Tap() => IsFinished = true;
    }
}