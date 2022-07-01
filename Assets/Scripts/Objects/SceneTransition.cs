using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Vector2 _playerPosition;
    [SerializeField] private VectorValue _playerPositionStorage;

    [Header("Objects")]
    [SerializeField] private GameObject _fadeOutPanel;
    [SerializeField] private GameObject _fadeInPanel;

    public void Awake()
    {
        if (_fadeOutPanel != null)
        {
            GameObject panel = Instantiate(_fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            StartCoroutine(FadeCoroutine());
            _playerPositionStorage.Value = _playerPosition;
        }
    }
    
    public IEnumerator FadeCoroutine()
    {
        if (_fadeOutPanel != null)
            Instantiate(_fadeInPanel, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(0.7f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);

        while (!asyncOperation.isDone)
            yield return null;
    }
}
