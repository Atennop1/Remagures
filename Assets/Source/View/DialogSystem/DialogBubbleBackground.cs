using UnityEngine;

namespace Remagures.View.DialogSystem
{
    public class DialogBubbleBackground : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject _bubble;

        private RectTransform _rect;
        private RectTransform _bubbleRect;

        private void Awake() 
        {
            _rect = GetComponent<RectTransform>();
            _bubbleRect = _bubble.GetComponent<RectTransform>();
        }
    
        private void FixedUpdate() 
        {
            _rect.sizeDelta = _bubbleRect.sizeDelta + new Vector2(0, 34f + (_bubbleRect.childCount - 2) * 10);
            _rect.position = _camera.WorldToScreenPoint(_playerTransform.position + new Vector3(0, 0.5f)) 
                                 + new Vector3(0, 75 + _rect.sizeDelta.y / 2 * 1.55f);

            //idk what is going on here... this is very very old code...
        }
    }
}
