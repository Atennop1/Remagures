using Remagures.Model.Flashing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class FlashingsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private IFlashingData _flashingData;
        private IFlashings _builtFlashings;
        
        public IFlashings Create()
        {
            if (_builtFlashings != null)
                return _builtFlashings;
            
            _builtFlashings = new Flashings(_spriteRenderer, _flashingData);
            return _builtFlashings;
        }
    }
}