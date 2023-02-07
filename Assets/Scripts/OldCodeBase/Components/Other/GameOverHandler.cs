using System.Collections;
using Remagures.SO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Remagures.Components
{
    public class GameOverHandler : MonoBehaviour
    {
        [Header("Colors")]
        [SerializeField] private Color _colorOfPlayer;
        [SerializeField] private Color _colorOfOther;

        [Space]
        [SerializeField] private GameObject _menuOfChoose;
        [SerializeField] private FloatValue _currentHealth;

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
            if (!_isGameOver || !(_currentTime < 1)) return;
        
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

        public void Init()
        {
            _spriteRenderers = FindObjectsOfType<SpriteRenderer>();
            _tilemaps = FindObjectsOfType<Tilemap>();
        }

        public void SetGameOver()
            => _isGameOver = _currentHealth.Value <= 0;
    }
}
