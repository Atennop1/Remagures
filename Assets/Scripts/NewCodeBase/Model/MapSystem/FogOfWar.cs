using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.MapSystem
{
    [RequireComponent(typeof(Image))]
    public class FogOfWar : MonoBehaviour
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.alphaHitTestMinimumThreshold = 0.6f;
        }
    }
}
