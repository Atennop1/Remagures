using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private StringValue _currentScene;
    [SerializeField] private BoolValue _isNewGame;

    [Header("Objects")]
    [SerializeField] private GameObject _newGameMenu;
    [SerializeField] private GameObject _oldGameMenu;
    [SerializeField] private GameObject _confirmNewGame;
    [SerializeField] private GameObject _chooseClassMenu;
    
    public void Start()
    {
        if (_isNewGame.Value)
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

    public void Sure()
    {
        if (_chooseClassMenu)
            _chooseClassMenu.SetActive(true);

        if (_confirmNewGame)
            _confirmNewGame.SetActive(false);
    }

    public void ChangeMind()
    {
        if (_chooseClassMenu)
            _chooseClassMenu.SetActive(false);
    }

    public void NewGame()
    {
        if (_confirmNewGame)
            _confirmNewGame.SetActive(true);
    }

    public void CloseNewGame()
    {
        if (_confirmNewGame)
            _confirmNewGame.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        SceneManager.LoadScene(_currentScene.Value);
    }
}
