using System;
using System.Collections.Generic;
using Remagures.Root;

namespace Remagures.Model.MapSystem
{
    public sealed class MapSelector : IUpdatable
    {
        public IMap CurrentLocationMap { get; } //TODO make this map open on the first click on the open map button
        public IMap CurrentlySelectedMap { get; private set; }
        
        private readonly List<IMap> _maps;

        public MapSelector(List<IMap> maps)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            CurrentLocationMap = _maps.Find(map => map.Markers.CharacterMarker.IsActive());
        }

        public void Update()
        {
            var mapWhichHasOpened = _maps.Find(map => map.HasOpened);

            if (mapWhichHasOpened != null)
                CurrentlySelectedMap = mapWhichHasOpened;
        }
    }
}