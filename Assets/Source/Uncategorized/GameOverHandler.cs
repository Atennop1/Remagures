using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Remagures.Uncategorized
{
    public sealed class GameOverHandler : MonoBehaviour //TODO make this via polling
    {
        [Header("Colors")]
        [SerializeField] private Color _colorOfPlayer;
        [SerializeField] private Color _colorOfOther;
        [SerializeField] private GameObject _menuOfChoose;

        private float _currentTime;
        private Coroutine _colorCoroutine;

        private bool _isGameOver;
        private SpriteRenderer[] _spriteRenderers;
        private Tilemap[] _tilemaps;

        private void OnEnable()
        {
            _spriteRenderers = null;
            _tilemaps = null;
        }

        public void Update()
        {
            if (!_isGameOver || !(_currentTime < 1)) 
                return;
        
            _colorCoroutine ??= StartCoroutine(Timer());

            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.Lerp(Color.white, 
                    spriteRenderer.gameObject.name != "Player Death(Clone)" 
                        ? _colorOfOther 
                        : _colorOfPlayer, _currentTime);
            }
            
            foreach (var tilemap in _tilemaps) 
                tilemap.color = Color.Lerp(Color.white, _colorOfOther, _currentTime);
        }

        private IEnumerator Timer()
        {
            while (_currentTime < 1)
            {
                _currentTime += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        
            yield return new WaitForSeconds(0.5f);
            if (_menuOfChoose)
                _menuOfChoose.SetActive(true);
        }

        private void Awake()
        {
            _spriteRenderers = FindObjectsOfType<SpriteRenderer>();
            _tilemaps = FindObjectsOfType<Tilemap>();
        }
    }
}
