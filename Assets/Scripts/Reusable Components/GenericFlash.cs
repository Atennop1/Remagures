using System.Collections;
using UnityEngine;

public class GenericFlash : MonoBehaviour
{
    [field: SerializeField, Header("Colors")] public Color RegularColor { get; private set; }
    [field: SerializeField] public Color DamageColor { get; private set; }
    [field: SerializeField] public Color FireColor { get; private set; }

    [Header("Values")]
    [SerializeField] private float _flashDuration;
    [SerializeField] private int _numberOfFlashes;

    [field: SerializeField, Header("Objects")] public SpriteRenderer SpriteRenderer { get; private set; }
    private Coroutine _flashCoroutine;

    public IEnumerator FlashCoroutine(Color flashColor, Color afterFlashColor)
    {
        int temp = 0;
        Color inFlashColor = SpriteRenderer.color;
            
        while (temp < _numberOfFlashes)
        {
            SpriteRenderer.color = flashColor;
            yield return new WaitForSeconds(_flashDuration);
            SpriteRenderer.color = inFlashColor;
            yield return new WaitForSeconds(_flashDuration);
            temp++;
        }

        SpriteRenderer.color = afterFlashColor;

        _flashCoroutine = null;
    }

    public void StopCoroutine(Color flashColor, Color afterFlashColor)
    {
        StopCoroutine(_flashCoroutine);
    }
    
    public void StartFlashCoroutine(Color flashColor, Color afterFlashColor)
    {
        if (gameObject.activeInHierarchy)
        {
            if (_flashCoroutine != null)
                StopCoroutine(_flashCoroutine);
            _flashCoroutine = StartCoroutine(FlashCoroutine(flashColor, afterFlashColor));
        }
    }
}
