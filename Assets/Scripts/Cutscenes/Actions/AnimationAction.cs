using UnityEngine;

namespace Remagures.Cutscenes.Actions
{
    public class AnimationAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished => !_animator.GetCurrentAnimatorStateInfo(0).IsName(_animationKey);
        
        private readonly string _animationKey;
        private readonly Animator _animator;
        
        public AnimationAction(Animator animator, string animationKey)
        {
            _animationKey = animationKey;
            _animator = animator;
        }

        public void Start()
        {
            IsStarted = true;
            _animator.Play(_animationKey);
        }
        
        public void Finish() { }
    }
}