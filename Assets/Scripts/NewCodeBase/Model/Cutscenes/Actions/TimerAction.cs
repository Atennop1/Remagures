using System;
using Cysharp.Threading.Tasks;

namespace Remagures.Model.Cutscenes
{
    public class TimerAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }

        private readonly float _delay;
        
        public TimerAction(float delay)
        {
            if (delay <= 0)
                throw new ArgumentException("Delay can't be less or equals zero");
            
            _delay = delay;
        }
        
        public async void Start()
        {
            IsStarted = true;
            await UniTask.Delay((int)(_delay * 1000));
            IsFinished = true;
        }
        
        public void Finish() { }
    }
}