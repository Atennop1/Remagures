using System;
using System.Collections.Generic;
using System.Linq;

namespace Remagures.Model.MapSystem
{
    public sealed class ParentMapOpener
    {
        private readonly List<IMap> _maps;
        private readonly IMapSelector _mapSelector;

        public ParentMapOpener(List<IMap> maps, IMapSelector mapSelector)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            _mapSelector = mapSelector ?? throw new ArgumentException(nameof(mapSelector));
        }

        public void Open()
        {
            var selectedMap = _mapSelector.SelectedMap;
            _maps.Find(map => map.Transitions.Any(transition => transition.MapToTransit == selectedMap))?.Open();
        }

        public bool CanOpen()
            => _maps.Find(map => map.Transitions.Any(transition => transition.MapToTransit == _mapSelector.SelectedMap)) != null;
    }
}