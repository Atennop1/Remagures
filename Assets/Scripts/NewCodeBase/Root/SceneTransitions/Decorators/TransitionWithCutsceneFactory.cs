using Remagures.Model.SceneTransition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TransitionWithCutsceneFactory : SerializedMonoBehaviour, ISceneTransitionFactory
    {
        [SerializeField] private ISceneTransitionFactory _transitionFactory;
        [SerializeField] private ICutsceneFactory _cutsceneFactory;
        private ISceneTransition _builtTransition;
        
        public ISceneTransition Create()
        {
            if (_builtTransition != null)
                return _builtTransition;

            _builtTransition = new TransitionWithCutscene(_transitionFactory.Create(), _cutsceneFactory.Create());
            return _builtTransition;
        }
    }
}