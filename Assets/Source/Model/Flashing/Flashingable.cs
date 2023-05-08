using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.Flashing
{
    public class Flashingable : IFlashingable
    {
        private readonly Dictionary<FlashColorType, Color> _colors;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IFlashings _flashings;
        
        private readonly List<Color> _currentFlashingsColors = new();

        public Flashingable(SpriteRenderer spriteRenderer, IFlashings flashings, Dictionary<FlashColorType, Color> colors)
        {
            _spriteRenderer = spriteRenderer ?? throw new ArgumentNullException(nameof(spriteRenderer));
            _colors = colors ?? throw new ArgumentNullException(nameof(colors));
            _flashings = flashings ?? throw new ArgumentNullException(nameof(flashings));
        }

        public void Flash(FlashColorType flashColorType, FlashColorType afterFlashColorType, FlashingData data)
        {
            if (afterFlashColorType != FlashColorType.BeforeFlash && !_colors.ContainsKey(afterFlashColorType))
                throw new ArgumentException($"This flashingable haven't color {afterFlashColorType}");
            
            if (afterFlashColorType != FlashColorType.BeforeFlash && !_currentFlashingsColors.Contains(_spriteRenderer.color))
                _currentFlashingsColors.Add(_spriteRenderer.color);

            var afterFlashColor = _colors[afterFlashColorType];

            if (afterFlashColorType == FlashColorType.BeforeFlash)
            {
                if (_currentFlashingsColors.Count > 0 && _currentFlashingsColors.Contains(_spriteRenderer.color)) //TODO check if this work in multiple simulations
                    _currentFlashingsColors.Remove(_currentFlashingsColors[^1]);
                
                afterFlashColor = _currentFlashingsColors.Count != 0 
                    ? _currentFlashingsColors[^1] 
                    : _colors[FlashColorType.Regular];
            }
            
            _flashings.Start(_colors[flashColorType], afterFlashColor, data);
        }
    }
}
