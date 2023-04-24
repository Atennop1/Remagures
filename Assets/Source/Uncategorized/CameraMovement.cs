using System.Collections;
using UnityEngine;

namespace Remagures.Uncategorized
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector2 _maxPos;
        [SerializeField] private Vector2 _minPos;
    
        private Animator _anim;
        private readonly int KICK_ANIMATOR_NAME = Animator.StringToHash("Kick");

        public void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void Update()
        {
            if (transform.position == _target.position) return;
            var targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minPos.x, _maxPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, _minPos.y, _maxPos.y);
        
            transform.position = targetPosition;
        }

        public void ScreenKick()
            => StartCoroutine(ScreenKickCoroutine());

        private IEnumerator ScreenKickCoroutine()
        {
            _anim.SetBool(KICK_ANIMATOR_NAME, true);
            yield return null;
            _anim.SetBool(KICK_ANIMATOR_NAME, false);
        }
    }
}
