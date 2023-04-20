using System;
using System.Collections.Generic;
using Remagures.Root;

namespace Remagures.Model.MapSystem
{
    public sealed class AutoMapSelector : IUpdatable, IMapSelector
    {
        public IMap CurrentLocationMap { get; }
        public IMap SelectedMap { get; private set; }
        
        private readonly List<IMap> _maps;

        public AutoMapSelector(List<IMap> maps)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            CurrentLocationMap = _maps.Find(map => map.Markers.CharacterMarker.IsActive());
            SelectedMap = CurrentLocationMap;
        }

        public void Update()
        {
            var openedMap = _maps.Find(map => map.HasOpened);

            if (openedMap != null)
                SelectedMap = openedMap;
        }
    }
}