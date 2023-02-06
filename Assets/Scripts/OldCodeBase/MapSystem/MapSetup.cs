using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Remagures.MapSystem
{
    public class MapSetup : MonoBehaviour
    {
        [SerializeField] private MapView _view;
        [SerializeField] private List<LocationMap> _maps;

        public LocationMap CurrentLocationMap { get; private set; }

        private void Awake()
        {
            foreach (var map in _maps.Where(map => map.GetMarker<PlayerMarker>().ContainsInMap(map)))
            {
                _view.OpenMap(map);
                CurrentLocationMap = map;
            }
        }
    }
}
