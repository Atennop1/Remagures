using System;
using Remagures.Factories;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public sealed class TransitionWithFade : ISceneTransition
    {
        private readonly ISceneTransition _sceneTransition;
        private readonly IGameObjectFactory _fadeGameObjectFactory;

        public TransitionWithFade(ISceneTransition sceneTransition, IGameObjectFactory fadeGameObjectFactory)
        {
            _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));
            _fadeGameObjectFactory = fadeGameObjectFactory ?? throw new ArgumentNullException(nameof(fadeGameObjectFactory));
        }

        public void Activate()
        {
            _sceneTransition.Activate();
            _fadeGameObjectFactory.Create(Vector3.zero);
        }
    }
}