using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Remagures.Model.Flashing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Remagures.Model.Health
{
    public sealed class FireHealthEffect : IHealthEffect
    {
        private readonly IHealth _health;
        private readonly IFlashingable _flash;
        private readonly FireInfo _info;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public FireHealthEffect(IHealth health, IFlashingable flash, FireInfo info)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _flash = flash ?? throw new ArgumentNullException(nameof(flash));
            _info = info;
        }

        public async void Activate()
            => await Fire(_flash, _info.Damage);

        public void Stop()
            => _cancellationTokenSource.Cancel();

        private async UniTask Fire(IFlashingable flash, float damage)
        {
            if (flash == null)
                throw new ArgumentNullException(nameof(flash));
            
            var randomTime = Random.Range(Mathf.RoundToInt(_info.MinTime), Mathf.RoundToInt(_info.MaxTime) + 1);

            for (var i = 0; i < randomTime; i++)
            {
                await UniTask.Delay(1000);

                if (_cancellationTokenSource.IsCancellationRequested)
                    break;

                _health.TakeDamage(Mathf.RoundToInt(damage / 2));
                var isLastFlash = i == randomTime - 1;
                
                flash.Flash(FlashColorType.Damage, isLastFlash ? FlashColorType.BeforeFlash : FlashColorType.Fire);
            }
        }
    }
}
