using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.MapSystem
{
    public sealed class MapCloseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _mapMenuGameObject;
        [SerializeField] private Button _closeButton;
        
        private void Close()
        {
            _mapMenuGameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void Awake()
            => _closeButton.onClick.AddListener(Close);

        private void OnDestroy()
            => _closeButton.onClick.RemoveListener(Close);
    }
}