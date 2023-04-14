using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MarkersFactory : SerializedMonoBehaviour, IMarkersFactory
    {
        [SerializeField] private IMarkerFactory _characterMarkerFactory;
        [SerializeField] private List<IMarkerFactory> _goalMarkersFactories;
        private IMarkers _builtMarkers;

        public IMarkers Create()
        {
            if (_builtMarkers != null)
                return _builtMarkers;

            _builtMarkers = new Markers(_characterMarkerFactory.Create(), _goalMarkersFactories.Select(factory => factory.Create()).ToList());
            return _builtMarkers;
        }
    }
}