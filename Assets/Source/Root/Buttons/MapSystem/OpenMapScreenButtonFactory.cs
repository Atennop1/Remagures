using Remagures.Model.Buttons;
using Remagures.View.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class OpenMapScreenButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private MapSelectorFactory _mapSelectorFactory;
        [SerializeField] private IMapView _mapView;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new OpenMapScreenButton(_mapSelectorFactory.Create(), _mapView);
            return _builtButton;
        }
    }
}