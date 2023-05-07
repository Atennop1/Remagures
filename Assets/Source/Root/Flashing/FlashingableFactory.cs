using System.Collections.Generic;
using Remagures.Model.Flashing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class FlashingableFactory : SerializedMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private FlashingsFactory _flashingsFactory;
        [SerializeField] private Dictionary<FlashColorType, Color> _colors;

        private IFlashingable _builtFlashingable;

        public IFlashingable Create()
        {
            if (_builtFlashingable != null)
                return _builtFlashingable;

            _builtFlashingable = new Flashingable(_spriteRenderer, _flashingsFactory.Create(), _colors);
            return _builtFlashingable;
        }
    }
}