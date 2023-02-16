using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Remagures.Model.SceneTransition
{
    public class SceneTransition : ISceneTransition
    {
        public bool HasActivated { get; private set; }

        private readonly string _sceneName;
        
        private const int FADING_DELAY_IN_MILLISECONDS = 1500;

        public SceneTransition(string sceneName)
            => _sceneName = sceneName ?? throw new ArgumentNullException(nameof(sceneName));

        public void LateUpdate()
            => HasActivated = false;

        public async void Activate()
        {
            HasActivated = true;
            await LoadScene();
        }

        private async UniTask LoadScene()
        {
            await UniTask.Delay(FADING_DELAY_IN_MILLISECONDS);
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}
