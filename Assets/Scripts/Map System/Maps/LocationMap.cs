using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMap : Map
{
    [Header("Location Stuff")]
    [SerializeField] private RectTransform _mapTransform;

    [Space]
    [SerializeField] private Vector2 _worldSize;
    [SerializeField] private Vector3 _playerMarkerScale;

    private Transform _player;

    public void Init(Transform player, MapView view)
    {
        _player = player;
        base.Init(view);
    }

    public override void DisplayPlayerMarker()
    {
        if (_player != null)
        {
            PlayerMarker.gameObject.SetActive(true);
            PlayerMarker.localScale = _playerMarkerScale;

            Vector3 markerNewPosition = _player.transform.position * (_mapTransform.sizeDelta / _worldSize);
            PlayerMarker.anchoredPosition = markerNewPosition;
        }
    }
}
