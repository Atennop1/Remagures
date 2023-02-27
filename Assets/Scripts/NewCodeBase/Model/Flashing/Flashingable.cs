using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.Flashing
{
    public class Flashingable : IFlashingable
    {
        private readonly Dictionary<FlashColorType, Color> _colors;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IFlashingsStarter _flashingsStarter;
        
        private readonly List<Color> _currentFlashingsBeforeColors = new();

        public Flashingable(SpriteRenderer spriteRenderer, IFlashingsStarter flashingsStarter, Dictionary<FlashColorType, Color> colors)
        {
            _spriteRenderer = spriteRenderer ?? throw new ArgumentNullException(nameof(spriteRenderer));
            _colors = colors ?? throw new ArgumentNullException(nameof(colors));
            _flashingsStarter = flashingsStarter ?? throw new ArgumentNullException(nameof(flashingsStarter));
        }

        public void Flash(FlashColorType flashColorType, FlashColorType afterFlashColorType)
        {
            if (afterFlashColorType != FlashColorType.BeforeFlash && !_colors.ContainsKey(afterFlashColorType))
                throw new ArgumentException($"This flashingable haven't color {afterFlashColorType}");
            
            if (afterFlashColorType != FlashColorType.BeforeFlash && !_currentFlashingsBeforeColors.Contains(_spriteRenderer.color))
                _currentFlashingsBeforeColors.Add(_spriteRenderer.color);

            var afterFlashColor = _colors[afterFlashColorType];

            if (afterFlashColorType == FlashColorType.BeforeFlash)
            {
                if (_currentFlashingsBeforeColors.Count > 0)
                    _currentFlashingsBeforeColors.Remove(_currentFlashingsBeforeColors[^1]);
                
                afterFlashColor = _currentFlashingsBeforeColors.Count != 0 
                    ? _currentFlashingsBeforeColors[^1] 
                    : _colors[FlashColorType.Regular];
            }
            
            _flashingsStarter.StartFlashing(_colors[flashColorType], afterFlashColor);
        }
    }
}
