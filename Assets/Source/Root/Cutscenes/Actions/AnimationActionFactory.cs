using Remagures.Model.CutscenesSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class AnimationActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationKey;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new AnimationAction(_animator, _animationKey);
            return _builtCutsceneAction;
        }
    }
}