using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private GameObject _meatCanvas;
    [SerializeField] private GameOverScript _gameOver;
    private GameSaveContainer _saveContainer;

    public void OnEnable()
    {
        _saveContainer = GameObject.Find("GameSaveManager").GetComponent<GameSaveContainer>();
    }

    public void ChangeScreen(GameObject gameObject)
    {
        bool active = !gameObject.activeInHierarchy;
        gameObject.SetActive(active);
        Time.timeScale = !active ? 1 : 0;
    }

    public void ShowMeatScreen()
    {
        _meatCanvas.SetActive(true);
    }

    public void ToMenu()
    {
        _gameOver.SetGameOver();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        _saveContainer.SaveGame();
    }

    public void Restart()
    {
        _gameOver.SetGameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _saveContainer.SaveGame();
    }
}
