using Remagures.Factories;
using Remagures.Model.SceneTransition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TransitionWithFadeFactory : SerializedMonoBehaviour, ISceneTransitionFactory
    {
        [SerializeField] private ISceneTransitionFactory _transitionFactory;
        [SerializeField] private IGameObjectFactory _fadeGameObjectFactory;
        private ISceneTransition _builtTransition;
        
        public ISceneTransition Create()
        {
            if (_builtTransition != null)
                return _builtTransition;

            _builtTransition = new TransitionWithFade(_transitionFactory.Create(), _fadeGameObjectFactory);
            return _builtTransition;
        }
    }
}