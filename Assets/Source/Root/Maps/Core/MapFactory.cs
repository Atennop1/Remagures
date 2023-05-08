using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Remagures.View.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapFactory : SerializedMonoBehaviour, IMapFactory
    {
        [SerializeField] private IMapView _mapView;
        [SerializeField] private IMarkersFactory _markersFactory;
        [SerializeField] private List<IMapTransitionFactory> _transitionsFactories;
        [SerializeField] private IIsMapVisitedFactory _isMapVisitedFactory;
        
        private Map _builtMap;
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();
        
        public IMap Create()
        {
            if (_builtMap != null)
                return _builtMap;

            _builtMap = new Map(_mapView, _markersFactory.Create(), _transitionsFactories.Select(factory => factory.Create()).ToList(), _isMapVisitedFactory.Create());
            _lateSystemUpdate.Add(_builtMap);
            return _builtMap;
        }
    }
}