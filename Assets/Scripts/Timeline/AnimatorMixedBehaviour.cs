using UnityEngine;
using UnityEngine.Playables;
using System;

[Serializable]
public class AnimatorMixerBehaviour : PlayableBehaviour
{
    [SerializeField] private float _moveX;
    [SerializeField] private float _moveY;
    [SerializeField] private string _boolName;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Animator animator = playerData as Animator;
        if (animator != null && animator.gameObject.activeInHierarchy && animator.runtimeAnimatorController) 
        {
            animator.SetFloat("moveX", _moveX);
            animator.SetFloat("moveY", _moveY);
            if (_boolName != "")
                animator.SetBool(_boolName, true);
        }
    }
}