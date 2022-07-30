using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    [SerializeField] private MapView _view;
    [SerializeField] private List<LocationMap> _maps;

    public LocationMap CurrentLocationMap { get; private set; }

    private void Awake()
    {
        foreach (LocationMap map in _maps)
        {
            if (map.GetMarker<PlayerMarker>().ContainsInMap(map))
            {
                _view.OpenMap(map);
                CurrentLocationMap = map;
            }
        }
    }
}
