using System.Collections;
using UnityEngine;

namespace Remagures.Components
{
    public class Flasher : MonoBehaviour
    {
        [field: SerializeField, Header("Colors")] public Color RegularColor { get; private set; }
        [field: SerializeField] public Color DamageColor { get; private set; }
        [field: SerializeField] public Color FireColor { get; private set; }

        [Header("Values")]
        [SerializeField] private float _flashDuration;
        [SerializeField] private int _numberOfFlashes;

        [field: SerializeField, Header("Objects")] public SpriteRenderer SpriteRenderer { get; private set; }
        private Coroutine _flashCoroutine;

        public void StartFlashCoroutine(Color flashColor, Color afterFlashColor)
        {
            if (!gameObject.activeInHierarchy)
                return;

            if (_flashCoroutine != null)
                StopCoroutine(_flashCoroutine);
            _flashCoroutine = StartCoroutine(FlashCoroutine(flashColor, afterFlashColor));
        }

        private IEnumerator FlashCoroutine(Color flashColor, Color afterFlashColor)
        {
            var currentFlashes = 0;
            var inFlashColor = SpriteRenderer.color;
            
            while (currentFlashes < _numberOfFlashes)
            {
                SpriteRenderer.color = flashColor;
                yield return new WaitForSeconds(_flashDuration);
            
                SpriteRenderer.color = inFlashColor;
                yield return new WaitForSeconds(_flashDuration);
                currentFlashes++;
            }

            SpriteRenderer.color = afterFlashColor;
            _flashCoroutine = null;
        }
    }
}
