using Remagures.Components;
using Remagures.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Remagures.Menu
{
    public class ScreenChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _meatCanvas;
        [SerializeField] private GameOverHandler _gameOver;
        [FormerlySerializedAs("_saveContainer")] [SerializeField] private GameSaver _gameSaver;

        public void ChangeScreen(GameObject screen)
        {
            var active = !screen.activeInHierarchy;
            screen.SetActive(active);
            UnityEngine.Time.timeScale = !active ? 1 : 0;
        }

        public void ShowMeatScreen()
            => _meatCanvas.SetActive(true);

        public void ToMenu()
        {
            _gameOver.SetGameOver();
            SceneManager.LoadScene("Menu");
            UnityEngine.Time.timeScale = 1;
            _gameSaver.SaveGame();
        }

        public void Restart()
        {
            _gameOver.SetGameOver();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            _gameSaver.SaveGame();
        }
    }
}
