using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Components.Base
{
    public class SmoothColorSwitcher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public async void SwitchTo(Color color, float time)
        {
            var timer = 0f;
            var startColor = _spriteRenderer.color;

            while (timer < time)
            {
                _spriteRenderer.color = Color.Lerp(startColor, color, timer / time);
                timer += UnityEngine.Time.fixedDeltaTime;
                await UniTask.WaitForFixedUpdate();
            }

            _spriteRenderer.color = color;
        }
    }
}