using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Components
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private Text _fpsText;

        private const float _pollingTime = 0.5f;
        private float _time;
        private int _framesCount;

        private void Update()
        {
            _time += UnityEngine.Time.deltaTime;
            _framesCount++;

            if (!(_time >= _pollingTime)) return;
        
            var frameRate = Mathf.RoundToInt(_framesCount / _time);
            _fpsText.text = frameRate.ToString();

            _time -= _pollingTime;
            _framesCount = 0;
        }
    }
}
