using System;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class MapTransition
    {
        public readonly IMap MapToOpen;
        private readonly MapView _view;

        public MapTransition(IMap mapToOpen, MapView view)
        {
            MapToOpen = mapToOpen ?? throw new ArgumentNullException(nameof(mapToOpen));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void OpenMap()
        {
            if (MapToOpen.IsVisited)
            {
                _view.OpenMap(MapToOpen);
            }
            else
            {
                _view.CantOpenMap();
            }
        }
    }
}
