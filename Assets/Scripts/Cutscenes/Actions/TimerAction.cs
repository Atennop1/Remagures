using Cysharp.Threading.Tasks;

namespace Remagures.Cutscenes.Actions
{
    public class TimerAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }

        private readonly float _delay;
        
        public TimerAction(float delay)
        {
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