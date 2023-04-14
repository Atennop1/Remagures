using Remagures.Model.SceneTransition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class SceneTransitionFactory : SerializedMonoBehaviour, ISceneTransitionFactory
    {
        [SerializeField] private string _name;
        private ISceneTransition _builtTransition;

        public ISceneTransition Create()
        {
            if (_builtTransition != null)
                return _builtTransition;

            _builtTransition = new SceneTransition(_name);
            return _builtTransition;
        }
    }
}