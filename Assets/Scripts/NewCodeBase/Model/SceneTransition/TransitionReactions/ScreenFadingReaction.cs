using Remagures.Factories;
using Remagures.Root;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public class ScreenFadingReaction : IUpdatable
    {
        private readonly ISceneTransition _sceneTransition;
        private readonly GameObjectFactory _fadeGameObjectFactory;
        
        public void Update()
        {
            if (_sceneTransition.HasActivated)
                _fadeGameObjectFactory.Create(Vector3.zero);
        }
    }
}