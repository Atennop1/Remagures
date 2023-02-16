using System;
using Remagures.Root;
using Remagures.View;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public class PlayerFadingReaction : IUpdatable
    {
        private readonly ISceneTransition _sceneTransition;
        private readonly SpriteRendererColorSwitcher _playerColorSwitcher;

        private const float TIME_OF_FADING = 0.5f;

        public PlayerFadingReaction(ISceneTransition sceneTransition, SpriteRendererColorSwitcher playerColorSwitcher)
        {
            _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));
            _playerColorSwitcher = playerColorSwitcher ?? throw new ArgumentNullException(nameof(playerColorSwitcher));
        }

        public void Update()
        {
            if (_sceneTransition.HasActivated)
                _playerColorSwitcher.SwitchTo(Color.black, TIME_OF_FADING);
        }
    }
}