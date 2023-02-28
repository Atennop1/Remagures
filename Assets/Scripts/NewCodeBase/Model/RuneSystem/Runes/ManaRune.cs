using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Remagures.Model.Magic;

namespace Remagures.Model.RuneSystem
{
    public sealed class ManaRune : IRune
    {
        public bool IsActive { get; private set; }

        private CancellationTokenSource _cancellationTokenSource;
        private readonly IMana _mana;

        public ManaRune(IMana mana)
            => _mana = mana ?? throw new ArgumentNullException(nameof(mana));
        
        
        public async void Activate()
        {
            IsActive = true;
            _cancellationTokenSource = new CancellationTokenSource();
            await StartAddingMana(_cancellationTokenSource.Token);
        }

        public void Deactivate()
        {
            IsActive = true;
            _cancellationTokenSource.Cancel();
        }

        private async UniTask StartAddingMana(CancellationToken cancellationToken)
        {
            while (true)
            {
                await Task.Delay(60000, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                _mana.Increase(_mana.MaxValue / 10);
            }
        }
    }
}