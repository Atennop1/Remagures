using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Remagures.Model.Magic;

namespace Remagures.Model.RuneSystem
{
    public sealed class ManaRune : IRune
    {
        private readonly IMana _mana;
        private CancellationTokenSource _cancellationTokenSource = new();

        private const int RUNE_COOLDOWN_IN_MILLISECONDS = 60000;

        public ManaRune(IMana mana)
            => _mana = mana ?? throw new ArgumentNullException(nameof(mana));

        public bool IsActive { get; private set; }
        
        public async void Activate()
        {
            IsActive = true;
            _cancellationTokenSource = new CancellationTokenSource();
            await ManaAddingCycle();
        }

        public void Deactivate()
        {
            IsActive = false;
            _cancellationTokenSource.Cancel();
        }

        private async UniTask ManaAddingCycle()
        {
            while (true)
            {
                await Task.Delay(RUNE_COOLDOWN_IN_MILLISECONDS, _cancellationTokenSource.Token);
                
                if (_cancellationTokenSource.IsCancellationRequested)
                    break;
                
                _mana.Increase(_mana.MaxValue / 10);
            }
        }
    }
}