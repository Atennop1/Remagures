using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.Flashing
{
    public sealed class Flashings : IFlashings
    {
        private readonly SpriteRenderer _spriteRenderer;
        private CancellationTokenSource _cancellationTokenSource = new();

        public Flashings(SpriteRenderer spriteRenderer) 
            => _spriteRenderer = spriteRenderer ?? throw new ArgumentNullException(nameof(spriteRenderer));

        public async void Start(Color flashColor, Color afterFlashColor, FlashingData data)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await FlashTask(flashColor, afterFlashColor, data);
        }
        
        private async UniTask FlashTask(Color flashColor, Color afterFlashColor, FlashingData data)
        {
            var currentFlashes = 0;
            var inFlashColor = _spriteRenderer.color;
            
            while (currentFlashes < data.NumberOfFlashes)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    break;
                
                _spriteRenderer.color = flashColor;
                await UniTask.Delay(data.FlashDurationInMilliseconds);
            
                _spriteRenderer.color = inFlashColor;
                await UniTask.Delay(data.FlashDurationInMilliseconds);
                
                currentFlashes++;
            }

            _spriteRenderer.color = afterFlashColor;
        }
    }
}