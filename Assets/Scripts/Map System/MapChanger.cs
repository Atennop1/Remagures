using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [field: SerializeField] public Map MapToOpen { get; private set; }
    [SerializeField] private Map _thisMap;

    [Space]
    [SerializeField] private Transform _playerMarkerTransform;

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

    public void DisplayMarkers()
    {
        foreach (IMarker marker in _thisMap.Markers)
            marker.TryDisplay(_playerMarkerTransform, _playerMarkerScale, MapToOpen);
    }

    public bool ContainsInMapTree<T>() where T: IMarker
    {
        return MapToOpen.GetMarker<T>().ContainsInMapTree(MapToOpen);
    }
}
