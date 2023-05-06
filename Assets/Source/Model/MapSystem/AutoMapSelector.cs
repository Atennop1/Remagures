using System;
using System.Collections.Generic;
using Remagures.Model.UI;
using Remagures.Root;

namespace Remagures.Model.MapSystem
{
    public sealed class AutoMapSelector : IUpdatable, IMapSelector
    {
        public IMap CurrentLocationMap { get; }
        public IMap SelectedMap { get; private set; }
        
        private readonly List<IMap> _maps;
        private readonly OpenParentMapButtonActivityChanger _openParentMapButtonActivityChanger; //TODO maybe throw this to another component

        public AutoMapSelector(List<IMap> maps, OpenParentMapButtonActivityChanger openParentMapButtonActivityChanger)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            _openParentMapButtonActivityChanger = openParentMapButtonActivityChanger ?? throw new ArgumentNullException(nameof(openParentMapButtonActivityChanger));
            
            CurrentLocationMap = _maps.Find(map => map.Markers.CharacterMarker.IsActive());
            SelectedMap = CurrentLocationMap;
        }

        public void Update()
        {
            var openedMap = _maps.Find(map => map.HasOpened);

            if (openedMap == null) 
                return;
            
            SelectedMap = openedMap;
            _openParentMapButtonActivityChanger.Change();
        }
    }
}