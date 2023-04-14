using System;
using System.Collections.Generic;
using Remagures.Root;

namespace Remagures.Model.MapSystem
{
    public sealed class MapSelector : IUpdatable
    {
        public IMap CurrentLocationMap { get; } //TODO make this map open on the first click on the open map button
        public IMap CurrentMap { get; private set; }
        
        private readonly List<IMap> _maps;

        public MapSelector(List<IMap> maps)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            CurrentLocationMap = _maps.Find(map => map.Markers.CharacterMarker.IsActive());
            CurrentMap = CurrentLocationMap;
        }

        public void Update()
        {
            var openedMap = _maps.Find(map => map.HasOpened);

            if (openedMap != null)
                CurrentMap = openedMap;
        }
    }
}