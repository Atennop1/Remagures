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

        public void ChangeScreen(GameObject screen)
        {
            var active = !screen.activeInHierarchy;
            screen.SetActive(active);
            Time.timeScale = !active ? 1 : 0;
        }

        public void ShowMeatScreen()
            => _meatCanvas.SetActive(true);

        public void ToMenu()
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
        }

        public void Restart() 
            => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
