using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameOverScript : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color _colorOfPlayer;
    [SerializeField] private Color _colorOfOther;

    [Space]
    [SerializeField] private GameObject _menuOfChoose;
    [SerializeField] private FloatValue _currentHealth;

    private float _currentTime = 0;
    private Coroutine _colorCoroutine;

    public bool IsGameOver { get; private set; }
    private SpriteRenderer[] _renderers;
    private Tilemap[] _tilemaps;

    private void OnEnable()
    {
        _renderers = null;
        _tilemaps = null;
    }

    public void Update()
    {
        if (IsGameOver && _currentTime < 1) 
        {
            if (_colorCoroutine == null)
                _colorCoroutine = StartCoroutine(Timer());
            foreach (SpriteRenderer renderer in _renderers) 
            {
                if (renderer.gameObject.name != "Player Death(Clone)")
                    renderer.color = Color.Lerp(Color.white, _colorOfOther, _currentTime);
                else
                    renderer.color = Color.Lerp(Color.white, _colorOfPlayer, _currentTime);
            }
            foreach (Tilemap tilemap in _tilemaps) 
                tilemap.color = Color.Lerp(Color.white, _colorOfOther, _currentTime);
        }
    }

    public IEnumerator Timer()
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
        _renderers = GameObject.FindObjectsOfType<SpriteRenderer>();
        _tilemaps = GameObject.FindObjectsOfType<Tilemap>();
    }

    public void SetGameOver()
    {
        IsGameOver = _currentHealth.Value <= 0;
    }
}
