using Remagures.Model.SceneTransition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TransitionWithPlayerFadingFactory : SerializedMonoBehaviour, ISceneTransitionFactory
    {
        [SerializeField] private ISceneTransitionFactory _transitionFactory;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ISceneTransition _builtTransition;
        
        public ISceneTransition Create()
        {
            if (_builtTransition != null)
                return _builtTransition;

            _builtTransition = new TransitionWithPlayerFading(_transitionFactory.Create(), _spriteRenderer);
            return _builtTransition;
        }
    }
}