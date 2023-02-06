using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Remagures.Assets;
using Remagures.QuestSystem;
using UnityEngine;

namespace Remagures.MapSystem
{
    public class Map : MonoBehaviour
    {
        [field: SerializeField] public List<SceneReference> MapScenes { get; private set; }
        [SerializeField] private List<GameObject> _markerObjects;

        [field: SerializeField, Space] public Map ParentMap { get; private set; }
        [field: SerializeField] protected RectTransform MapImageTransform { get; private set; }

        public IEnumerable<IMarker> Markers => _markers;
        private List<IMarker> _markers;

        public List<MapChanger> Changers { get; private set; }

        public T GetMarker<T>() where T: IMarker
        {
            SetupMarkers();
            foreach (var marker in _markers.OfType<T>())
                return marker;

            throw new ArgumentException("Map doesn't contains marker with type " + typeof(T));
        }

        public List<T> GetMarkers<T>() where T: IMarker
        {
            SetupMarkers();
            return _markers.OfType<T>().ToList();
        }

        public void Init(MapView view, QuestGoalsView goalsView)
        {
            Setup();
            
            foreach (var marker in _markers)
            {
                marker.Init(view);
                (marker as QuestMarker)?.GoalsInit(goalsView);
            }

            foreach (var changer in Changers)
                changer.Init(view);

            if (gameObject.activeInHierarchy)
                StartCoroutine(InitCoroutine());
        }

        public bool IsVisited()
        {
            return MapScenes.Count == 0 || MapScenes.Any(scene => PlayerPrefs.HasKey("Visited" + scene.ScenePath));
        }

        public void SetupChangers()
        {
            Changers = new List<MapChanger>();
            foreach (Transform child in MapImageTransform)
                if (child.gameObject.TryGetComponent(out MapChanger changer))
                    Changers.Add(changer);
        }

        public void SetupMarkers()
        {
            _markers = new List<IMarker>();
            foreach (var obj in _markerObjects)
                if (obj.TryGetComponent(out IMarker marker))
                    _markers.Add(marker);
        }
        
        protected virtual void DisplayPlayerMarker() { }
        
        private void Setup()
        {
            SetupMarkers();
            SetupChangers();
        }
        
        private void OnEnable()
        {
            Setup();
            
            foreach (var changer in Changers)
                changer.DisplayMarkers();

            if (GetMarker<PlayerMarker>().ContainsInMap(this))       
                DisplayPlayerMarker();
        }

        private IEnumerator InitCoroutine()
        {
            yield return new WaitForEndOfFrame();
            OnEnable();
        }
    }
}
