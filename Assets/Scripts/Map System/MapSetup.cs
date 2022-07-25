using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    [SerializeField] private MapView _view;
    [SerializeField] private List<LocationMap> _maps;

    private void Start()
    {
        foreach (LocationMap map in _maps)
            if (map.ContainsPlayerOnMap())
                _view.OpenMap(map);
    }
}
