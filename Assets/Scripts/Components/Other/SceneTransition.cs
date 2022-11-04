using System.Collections;
using Remagures.SO.PlayerStuff;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Components.Other
{
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
            if (_fadeOutPanel == null) return;
        
            var panel = Instantiate(_fadeOutPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player.Player _) || collision.isTrigger) return;
        
            StartCoroutine(FadeCoroutine());
            _playerPositionStorage.SetValue(_playerPosition);
        }

        private IEnumerator FadeCoroutine()
        {
            if (_fadeOutPanel != null)
                Instantiate(_fadeInPanel, Vector3.zero, Quaternion.identity);

            yield return new WaitForSeconds(0.7f);
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);

            while (!asyncOperation.isDone)
                yield return null;
        }
    }
}
