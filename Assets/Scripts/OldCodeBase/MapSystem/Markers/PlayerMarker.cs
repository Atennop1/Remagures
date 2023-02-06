using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.MapSystem
{
    public class PlayerMarker : MonoBehaviour, IMarker
    {
        [SerializeField] private Transform _playerMarker;
        public IMarkerVisitor Visitor { get; private set; }

        public void Init(MapView view) { }

        public void SetupComponents()
            => Visitor = new PlayerMarkerVisitor();

        public bool ContainsInMap(Map map)
            => map.MapScenes.Any(scene => scene.ScenePath == SceneManager.GetActiveScene().path);

        public bool ContainsInMapTree(Map map)
        {
            map.SetupChangers();
            return ContainsInMap(map) || map.Changers.Any(changer => changer.ContainsInMapTree<PlayerMarker>());
        }

        public bool TryDisplay(Transform parent, Vector3 scale, Map map)
        {
            _playerMarker.gameObject.SetActive(false);
            if (!ContainsInMapTree(map)) return false;

            _playerMarker.localScale = scale;
            _playerMarker.gameObject.SetActive(true);
            _playerMarker.SetParent(parent);

            map.ParentMap.SetupMarkers();
            foreach (var marker in map.ParentMap.Markers)
            {
                marker.SetupComponents();
                marker.Visitor.Visit(this);
            }

            return true;
        }

        public void SetPosition(Vector3 position)
            => ((RectTransform)_playerMarker).anchoredPosition = position;

        private class PlayerMarkerVisitor : IMarkerVisitor
        {
            public void Visit(IMarker marker)
            {
            }
        }
    }
}
