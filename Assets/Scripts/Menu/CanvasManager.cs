using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _meatCanvas;
    [SerializeField] private GameOverScript _gameOver;
    private GameSaveManager _saveManager;

    public void OnEnable()
    {
        _saveManager = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>();
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
        _saveManager.SaveGame();
    }

    public void Restart()
    {
        _gameOver.SetGameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _saveManager.SaveGame();
    }
}
