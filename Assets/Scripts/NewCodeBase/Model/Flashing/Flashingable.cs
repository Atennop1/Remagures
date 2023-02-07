using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.Flashing
{
    public class Flashingable : IFlashingable
    {
        private readonly Dictionary<FlashColorType, Color> _colors;
        private readonly FlashingData _flashingData;
        
        private readonly SpriteRenderer _spriteRenderer;
        private CancellationTokenSource _cancellationTokenSource = new();

        public Flashingable(SpriteRenderer spriteRenderer, Dictionary<FlashColorType, Color> colors, FlashingData flashingData)
        {
            _spriteRenderer = spriteRenderer ?? throw new ArgumentNullException(nameof(spriteRenderer));
            _colors = colors ?? throw new ArgumentNullException(nameof(colors));
            _flashingData = flashingData;
        }

        public async void Flash(FlashColorType flashColor, FlashColorType afterColorType)
        {
            if (!_colors.ContainsKey(flashColor))
                throw new ArgumentException($"This flashingable haven't color {flashColor}");
            
            if (afterColorType != FlashColorType.BeforeFlash && !_colors.ContainsKey(afterColorType))
                throw new ArgumentException($"This flashingable haven't color {afterColorType}");
            
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            var afterColor = afterColorType == FlashColorType.BeforeFlash ? _spriteRenderer.color : _colors[afterColorType];
            await FlashCoroutine(_colors[flashColor], afterColor);
        }

        private async UniTask FlashCoroutine(Color flash, Color afterColor)
        {
            var currentFlashes = 0;
            var inFlashColor = _spriteRenderer.color;
            
            while (currentFlashes < _flashingData.NumberOfFlashes)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    break;
                
                _spriteRenderer.color = flash;
                await UniTask.Delay(_flashingData.FlashDurationInMilliseconds);
            
                _spriteRenderer.color = inFlashColor;
                await UniTask.Delay(_flashingData.FlashDurationInMilliseconds);
                
                currentFlashes++;
            }

            _spriteRenderer.color = afterColor;
        }
    }
}
