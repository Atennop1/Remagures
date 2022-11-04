using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Remagures.Timeline.Behaviours
{
    [Serializable]
    public class RuntimeAnimatingBehaviour : PlayableBehaviour
    {
        [SerializeField] private float _moveX;
        [SerializeField] private float _moveY;
        [SerializeField] private string _boolName;
    
        private readonly int MOVE_X_ANIMATOR_NAME = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATOR_NAME = Animator.StringToHash("moveY");

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var animator = playerData as Animator;
            if (animator == null || !animator.gameObject.activeInHierarchy || !animator.runtimeAnimatorController) return;
        
            animator.SetFloat(MOVE_X_ANIMATOR_NAME, _moveX);
            animator.SetFloat(MOVE_Y_ANIMATOR_NAME, _moveY);
        
            if (_boolName != "")
                animator.SetBool(_boolName, true);
        }
    }
}