using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Components
{
    public class FireHealthEffect : IHealthEffect
    {
        private readonly Health _health;
        private readonly Flasher _flash;
        private readonly FireInfo _info;

        private readonly CancellationToken _cancellationToken;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public FireHealthEffect(Health health, Flasher flash, FireInfo info)
        {
            _health = health;
            _flash = flash;
            _info = info;

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        public async void Activate()
        {
            await Fire(_flash, _info.Damage, _cancellationToken);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTask Fire(Flasher flash, float damage, CancellationToken cancellationToken)
        {
            if (flash == null)
                return;

            flash.SpriteRenderer.color = flash.FireColor;
            var randomTime = Random.Range(Mathf.RoundToInt(_info.MinTime), Mathf.RoundToInt(_info.MaxTime) + 1);

            for (var i = 0; i < randomTime; i++)
            {
                await UniTask.Delay(1000, cancellationToken: cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    return;

                _health.TakeDamage(Mathf.RoundToInt(damage / 2));
                flash.StartFlashCoroutine(flash.DamageColor, i == randomTime - 1 ? flash.RegularColor : flash.FireColor);
            }
            
            _health.UnCastHealthEffect(this);
        }
    }
}
