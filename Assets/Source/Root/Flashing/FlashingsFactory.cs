using Remagures.Model.Flashing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class FlashingsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private IFlashingData _flashingData;
        
        public IFlashings Create() 
            => new Flashings(_spriteRenderer, _flashingData);
    }
}