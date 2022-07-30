using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationMap : Map
{
    [field: SerializeField, Header("Location Stuff")] public Texture2D ExplorationTexture { get; private set; }
    [field: SerializeField] public RectTransform PlayerMarker { get; private set; }

    [Space]
    [SerializeField] private Vector2 _worldSize;
    [SerializeField] private Vector2 _worldOffset;

    [Space]
    [SerializeField] private Vector3 _playerMarkerScale;

    private Transform _player;

    public void Init(MapView view, QuestGoalsView goalsView, Transform player)
    {
        base.Init(view, goalsView);
        _player = player;
    }

    public override void DisplayPlayerMarker()
    {
        if (_player != null)
        {
            PlayerMarker.SetParent(MapImageTransform);
            PlayerMarker.gameObject.SetActive(true);
            PlayerMarker.localScale = _playerMarkerScale;

            Vector3 markerNewPosition = _player.transform.position * (MapImageTransform.sizeDelta / _worldSize);
            PlayerMarker.anchoredPosition = markerNewPosition;
        }
    }

    public Vector2 CalculatePositionOnTexture()
    {
        return (_player.transform.position + (Vector3)(_worldSize / 2 + _worldOffset)) * 
        (new Vector2(ExplorationTexture.width, ExplorationTexture.height) / _worldSize);
    }
}
