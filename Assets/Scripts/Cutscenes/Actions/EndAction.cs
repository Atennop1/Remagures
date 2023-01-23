using System;
using Remagures.Interactable;

namespace Remagures.Cutscenes.Actions
{
    public class EndAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        
        private readonly UIActivityChanger _uiActivityChanger;

        public EndAction(UIActivityChanger uiActivityChanger)
        {
            _uiActivityChanger = uiActivityChanger ? uiActivityChanger 
                : throw new ArgumentException("UIActivityChanger can't be null");
        }
        
        public void Start()
        {
            IsStarted = true;
            IsFinished = true;
        }

        public void Finish() 
            => _uiActivityChanger.TurnOn();
    }
}