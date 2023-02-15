using System.Threading;
using Cysharp.Threading.Tasks;

namespace Remagures.Model.Magic
{
    public sealed class ManaRune 
    {
        private readonly IMana _mana;
        private CancellationTokenSource _cancellationTokenSource = new();
        private UniTask _manaAddingTask;
        
        private const int RUNE_COOLDOWN_IN_MILLISECONDS = 60000;

        public void Activate()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _manaAddingTask = ManaAddingCycle();
        }

        public void Deactivate()
        {
            if (_manaAddingTask.Status == UniTaskStatus.Pending)
                _cancellationTokenSource.Cancel();
        }

        private async UniTask ManaAddingCycle()
        {
            while (true)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    break;
                
                await UniTask.Delay(RUNE_COOLDOWN_IN_MILLISECONDS);
                _mana.Increase(_mana.MaxValue / 10);
            }
        }
    }
}