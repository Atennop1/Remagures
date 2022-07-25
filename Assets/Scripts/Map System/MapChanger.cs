using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [field: SerializeField] public Map MapToOpen { get; private set; }

    [Space]
    [SerializeField] private Transform _playerMarkerPosition;
    [SerializeField] private Transform _playerMarker;

    [Space]
    [SerializeField] private Vector3 _playerMarkerScale;

    private MapView _view;

    public void Init(MapView view)
    {
        _view = view;
    }

    public void OpenMap()
    {   
        if (MapToOpen.IsVisited())
            _view.OpenMap(MapToOpen);
        else
            _view.CantOpenMap();
    }

    public void DisplayPlayerMarker()
    {
        if (MapToOpen.ContainsPlayerInMapTree())
        {
            _playerMarker.gameObject.SetActive(true);
            _playerMarker.position = _playerMarkerPosition.position;
            _playerMarker.localScale = _playerMarkerScale;
        }
    }
}
