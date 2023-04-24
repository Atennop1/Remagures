using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public sealed class TransitionWithPlayerFading : ISceneTransition
    {
        private readonly ISceneTransition _sceneTransition;
        private readonly SpriteRenderer _playerSpriteRenderer;

        private const float TIME_OF_FADING = 0.5f;

        public TransitionWithPlayerFading(ISceneTransition sceneTransition, SpriteRenderer playerSpriteRenderer)
        {
            _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));
            _playerSpriteRenderer = playerSpriteRenderer ?? throw new ArgumentNullException(nameof(playerSpriteRenderer));
        }

        public async void Activate()
        {
            _sceneTransition.Activate();
            await SwitchColor(Color.black, TIME_OF_FADING);
        }
        
        private async UniTask SwitchColor(Color color, float time)
        {
            var timer = 0f;
            var startColor = _playerSpriteRenderer.color;

            while (timer < time)
            {
                _playerSpriteRenderer.color = Color.Lerp(startColor, color, timer / time);
                timer += Time.fixedDeltaTime;
                await UniTask.WaitForFixedUpdate();
            }

            _playerSpriteRenderer.color = color;
        }
    }
}