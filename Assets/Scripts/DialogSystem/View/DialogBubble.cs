using UnityEngine;

namespace Remagures.DialogSystem.View
{
    public class DialogBubble : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _bubble;

        private RectTransform _thisRect;
        private RectTransform _bubbleRect;

        private void Start() 
        {
            _thisRect = GetComponent<RectTransform>();
            _bubbleRect = _bubble.GetComponent<RectTransform>();
        }
    
        private void FixedUpdate() 
        {
            _thisRect.sizeDelta = _bubbleRect.sizeDelta + new Vector2(0, 34f + (_bubbleRect.childCount - 2) * 10);
            _thisRect.position = _camera.WorldToScreenPoint(_player.transform.position + new Vector3(0, 75.5f + _thisRect.sizeDelta.y * 0.775f, 0));

            //idk what is going on here... this is very old code...
        }
    }
}
