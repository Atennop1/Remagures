using SaveSystem;
using SaveSystem.Paths;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Menu
{
    public sealed class MainMenuButtons : MonoBehaviour 
    {
        [SerializeField] private GameObject _newGameMenu;
        [SerializeField] private GameObject _oldGameMenu;
        [SerializeField] private GameObject _confirmNewGame;
        [SerializeField] private GameObject _chooseClassMenu;

        private readonly ISaveStorage<bool> _isNewGameStorage = new BinaryStorage<bool>(new Path("IsNewGameStarted"));
        private readonly ISaveStorage<string> _currentSceneStorage = new BinaryStorage<string>(new Path("CurrentScene"));

        private void Start()
        {
            Application.targetFrameRate = 60;
            if (!_isNewGameStorage.HasSave() || _isNewGameStorage.Load())
            {
                _newGameMenu.SetActive(true);
                _oldGameMenu.SetActive(false);
            }
            else
            {
                _newGameMenu.SetActive(false);
                _oldGameMenu.SetActive(true);
            }
        }

        public void ConfirmNewGameStarting()
        {
            _chooseClassMenu.SetActive(true); 
            _confirmNewGame.SetActive(false);
        }

        public void CancelNewGameStarting() 
            => _chooseClassMenu.SetActive(false);

        public void RequestNewGameStarting() 
            => _confirmNewGame.SetActive(true);

        public void CloseNewGamePanel() 
            => _confirmNewGame.SetActive(false);

        public void Quit()
            => Application.Quit();

        public void Resume()
            => SceneManager.LoadScene(_currentSceneStorage.HasSave() ? _currentSceneStorage.Load() : "StartCutscene");
    }
}
