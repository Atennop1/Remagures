using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.MapSystem
{
    [RequireComponent(typeof(Image))]
    public sealed class FogOfWarView : SerializedMonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.alphaHitTestMinimumThreshold = 0.6f;
        }
    }
}
