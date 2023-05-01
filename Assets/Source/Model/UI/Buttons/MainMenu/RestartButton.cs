using SaveSystem;
using SaveSystem.Paths;
using UnityEngine.SceneManagement;

namespace Remagures.Model.UI
{
    public sealed class RestartButton : IButton
    {
        private readonly ISaveStorage<string> _currentSceneStorage = new BinaryStorage<string>(new Path("CurrentScene"));
        
        public void Press()
            => SceneManager.LoadScene(_currentSceneStorage.HasSave() ? _currentSceneStorage.Load() : "StartCutscene");
    }
}