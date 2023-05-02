﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.Flashing
{
    public class Flashings : IFlashings
    {
        private readonly IFlashingData _flashingData;
        private readonly SpriteRenderer _spriteRenderer;
        
        private CancellationTokenSource _cancellationTokenSource = new();

        public Flashings(SpriteRenderer spriteRenderer, IFlashingData flashingData)
        {
            _flashingData = flashingData;
            _spriteRenderer = spriteRenderer ?? throw new ArgumentNullException(nameof(spriteRenderer));
        }

        public async void Start(Color flashColor, Color afterFlashColor)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await FlashTask(flashColor, afterFlashColor);
        }
        
        private async UniTask FlashTask(Color flashColor, Color afterFlashColor)
        {
            var currentFlashes = 0;
            var inFlashColor = _spriteRenderer.color;
            
            while (currentFlashes < _flashingData.NumberOfFlashes)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    break;
                
                _spriteRenderer.color = flashColor;
                await UniTask.Delay(_flashingData.FlashDurationInMilliseconds);
            
                _spriteRenderer.color = inFlashColor;
                await UniTask.Delay(_flashingData.FlashDurationInMilliseconds);
                
                currentFlashes++;
            }

            _spriteRenderer.color = afterFlashColor;
        }
    }
}