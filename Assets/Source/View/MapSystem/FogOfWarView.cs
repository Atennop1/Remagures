using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.MapSystem
{
    [RequireComponent(typeof(Image))]
    public sealed class FogOfWarView : MonoBehaviour
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.alphaHitTestMinimumThreshold = 0.6f;
        }
    }
}
