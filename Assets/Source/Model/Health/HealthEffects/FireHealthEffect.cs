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
        private readonly IFlashingable _flashingable;
        private readonly FireInfo _fireInfo;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly FlashingData _flashingData = new(250, 2);

        public FireHealthEffect(IHealth health, IFlashingable flashingable, FireInfo fireInfo)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _flashingable = flashingable ?? throw new ArgumentNullException(nameof(flashingable));
            _fireInfo = fireInfo;
        }

        public async void Activate()
            => await Fire(_fireInfo.Damage);

        public void Stop()
            => _cancellationTokenSource.Cancel();

        private async UniTask Fire(float damage)
        {
            var randomTime = Random.Range(Mathf.RoundToInt(_fireInfo.MinTime), Mathf.RoundToInt(_fireInfo.MaxTime) + 1);

            for (var i = 0; i < randomTime; i++)
            {
                await UniTask.Delay(1000);

                if (_cancellationTokenSource.IsCancellationRequested)
                    break;

                _health.TakeDamage(Mathf.RoundToInt(damage / 2));
                var isLastFlash = i == randomTime - 1;
                
                _flashingable.Flash(FlashColorType.Damage, isLastFlash ? FlashColorType.BeforeFlash : FlashColorType.Fire, _flashingData);
            }
        }
    }
}
