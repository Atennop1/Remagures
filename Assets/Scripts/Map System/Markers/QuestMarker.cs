using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        foreach (GoalMarker marker in GoalMarkers)
            marker.Init(goalsView);
    }

    public void SetupComponents()
    { 
        GoalMarkers = new List<GoalMarker>();
        foreach (Transform child in _goalsContainer)
            if (child.gameObject.TryGetComponent<GoalMarker>(out GoalMarker _marker))
                GoalMarkers.Add(_marker);

        Visitor = new QuestMarkerVisitor(_goalImage);
    }

    public bool ContainsInMap(Map map)
    {
        List<QuestMarker> questMarkers = map.GetMarkers<QuestMarker>();
        foreach (QuestMarker questMarker in questMarkers)
        {
            questMarker.SetupComponents();

            foreach (GoalMarker marker in questMarker.GoalMarkers)
                if (marker.IsGoalActive())
                    return true;
        }
            
        return false;
    }

    public bool ContainsInMapTree(Map map)
    { 
        if (ContainsInMap(map))
            return true;
        
        foreach (MapChanger changer in map.Changers)
        {
            List<QuestMarker> markers = changer.MapToOpen.GetMarkers<QuestMarker>();
            foreach (QuestMarker marker in markers)
                if (marker.ContainsInMapTree(changer.MapToOpen))
                    return true;
        }
        
        return false;
    }

    public bool TryDisplay(Transform parent, Vector3 scale, Map map)
    { 
        _goalImage.enabled = false;
        if (ContainsInMapTree(map))
        {
            _goalImage.gameObject.SetActive(true);
            _goalImage.enabled = true;

            return true;
        }

        return false;
    }

    private void Awake()
    {
        SetupComponents();
    }

    private class QuestMarkerVisitor : IMarkerVisitor
    {
        private Image _goalImage;

        public QuestMarkerVisitor(Image goalImage)
        {
            _goalImage = goalImage;
        }

        public void Visit(IMarker marker)
        {
            Visit((dynamic)marker);
        }

        public void Visit(PlayerMarker marker)
        {
            _goalImage.gameObject.SetActive(false);
        }
    }
}
