using Cysharp.Threading.Tasks;
using Remagures.Tools;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class WaitAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }

        private readonly float _delay;
        
        public WaitAction(float delay)
            => _delay = delay.ThrowExceptionIfLessOrEqualsZero();

        public async void Start()
        {
            IsStarted = true;
            await UniTask.Delay((int)(_delay * 1000));
            IsFinished = true;
        }
        
        public void Finish() { }
    }
}