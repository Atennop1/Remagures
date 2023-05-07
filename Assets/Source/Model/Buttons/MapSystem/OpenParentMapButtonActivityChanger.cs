using System;
using System.Collections.Generic;
using Remagures.Model.MapSystem;
using Remagures.Root;
using UnityEngine.UI;

namespace Remagures.Model.Buttons
{
    public sealed class OpenParentMapButtonActivityChanger : IUpdatable
    {
        private readonly List<IMap> _maps;
        private readonly ParentMapOpener _parentMapOpener;
        private readonly Button _button;

        public OpenParentMapButtonActivityChanger(List<IMap> maps, ParentMapOpener parentMapOpener, Button button)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            _parentMapOpener = parentMapOpener ?? throw new ArgumentNullException(nameof(parentMapOpener));
            _button = button ?? throw new ArgumentNullException(nameof(button));
        }

        public void Update()
        {
            if (_maps.Find(map => map.HasOpened) != null) 
                _button.enabled = _parentMapOpener.CanOpen();
        }
    }
}