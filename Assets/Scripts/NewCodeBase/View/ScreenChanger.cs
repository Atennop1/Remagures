using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.View
{
    public sealed class ScreenChanger : MonoBehaviour //TODO transform this to buttons classes
    {
        [SerializeField] private GameObject _meatCanvas;

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
