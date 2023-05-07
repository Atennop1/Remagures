using System;
using Remagures.Model.MapSystem;
using Remagures.View.MapSystem;

namespace Remagures.Model.Buttons
{
    public sealed class OpenMapScreenButton : IButton
    {
        private readonly IMapSelector _mapSelector;
        private readonly IMapView _mapView;
        private bool _isPressed;

        public OpenMapScreenButton(IMapSelector mapSelector, IMapView mapView)
        {
            _mapSelector = mapSelector ?? throw new ArgumentNullException(nameof(mapSelector));
            _mapView = mapView ?? throw new ArgumentNullException(nameof(mapView));
        }

        public void Press()
        {
            if (_isPressed)
                return;
            
            _mapView.Display(_mapSelector.CurrentLocationMap);
            _isPressed = true;
        }
    }
}