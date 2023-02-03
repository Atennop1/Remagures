using UnityEngine;

namespace Remagures.MapSystem
{
    public interface IMarker
    {
        public IMarkerVisitor Visitor { get; }

        public void Init(MapView view);
        public void SetupComponents();
        public bool ContainsInMapTree(Map map);

        public bool TryDisplay(Transform parent, Vector3 scale, Map map);
    }
}
