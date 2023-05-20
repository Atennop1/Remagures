using System;
using Remagures.Model.Interactable;
using Remagures.View;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class EndAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        
        private readonly IUIActivityChanger _uiActivityChanger;

        public EndAction(IUIActivityChanger uiActivityChanger) 
            => _uiActivityChanger = uiActivityChanger ?? throw new ArgumentException("UIActivityChanger can't be null");

        public void Start()
        {
            IsStarted = true;
            IsFinished = true;
        }

        public void Finish() 
            => _uiActivityChanger.TurnOn();
    }
}