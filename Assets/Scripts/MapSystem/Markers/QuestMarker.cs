using System.Collections.Generic;
using System.Linq;
using Remagures.QuestSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.MapSystem
{
    public class QuestMarker : MonoBehaviour, IMarker
    {
        [SerializeField] private Image _goalImage;
        [SerializeField] private Transform _goalsContainer;
        public List<GoalMarker> GoalMarkers { get; private set; }

        public IMarkerVisitor Visitor { get; private set; }

        public void Init(MapView view) { }

        public void GoalsInit(QuestGoalsView goalsView)
        {
            SetupComponents();
            foreach (var marker in GoalMarkers)
                marker.Init(goalsView);
        }

        public void SetupComponents()
        {
            GoalMarkers = new List<GoalMarker>();
            foreach (Transform child in _goalsContainer)
                if (child.gameObject.TryGetComponent(out GoalMarker _marker))
                    GoalMarkers.Add(_marker);

            Visitor = new QuestMarkerVisitor(_goalImage);
        }

        public bool ContainsInMap(Map map)
        {
            var questMarkers = map.GetMarkers<QuestMarker>();
            foreach (var questMarker in questMarkers)
            {
                questMarker.SetupComponents();

                if (questMarker.GoalMarkers.Any(marker => marker.IsGoalActive()))
                    return true;
            }

            return false;
        }

        public bool ContainsInMapTree(Map map)
        {
            if (ContainsInMap(map))
                return true;

            foreach (var changer in map.Changers)
            {
                var markers = changer.MapToOpen.GetMarkers<QuestMarker>();
                if (markers.Any(marker => marker.ContainsInMapTree(changer.MapToOpen)))
                    return true;
            }

            return false;
        }

        public bool TryDisplay(Transform parent, Vector3 scale, Map map)
        {
            _goalImage.enabled = false;
            if (!ContainsInMapTree(map)) return false;

            _goalImage.gameObject.SetActive(true);
            _goalImage.enabled = true;

            return true;
        }

        private void Awake()
            => SetupComponents();

        private class QuestMarkerVisitor : IMarkerVisitor
        {
            private readonly Image _goalImage;

            public QuestMarkerVisitor(Image goalImage)
                => _goalImage = goalImage;

            public void Visit(IMarker marker)
                => Visit((dynamic)marker);

            private void Visit(PlayerMarker _)
                => _goalImage.gameObject.SetActive(false);
        }
    }
}
