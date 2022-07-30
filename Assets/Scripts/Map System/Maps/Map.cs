using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Map : MonoBehaviour
{
    [field: SerializeField] public List<SceneReference> MapScenes { get; private set; }
    [SerializeField] private List<GameObject> _markerObjects;

    [field: SerializeField, Space] public Map ParentMap { get; private set; }
    [field: SerializeField] protected RectTransform MapImageTransform { get; private set; }

    public IReadOnlyList<IMarker> Markers => _markers;
    private List<IMarker> _markers;

    public List<MapChanger> Changers { get; private set; }

    public T GetMarker<T>() where T: IMarker
    {
        SetupMarkers();
        foreach (IMarker marker in _markers)
            if (marker is T)
                return (T)marker;

        throw new ArgumentException("Map doesn't contains marker with type " + typeof(T));
    }

    public List<T> GetMarkers<T>() where T: IMarker
    {
        SetupMarkers();
        List<T> markersList = new List<T>();
        foreach (IMarker marker in _markers)
            if (marker is T)
                markersList.Add((T)marker);

        return markersList;
    }

    public void Init(MapView view, QuestGoalsView goalsView)
    {
        foreach (IMarker marker in _markers)
        {
            marker.Init(view);
            (marker as QuestMarker)?.GoalsInit(goalsView);
        }

        foreach (MapChanger changer in Changers)
            changer.Init(view);

        if (gameObject.activeInHierarchy)
            StartCoroutine(InitCoroutine());
    }

    public bool IsVisited()
    {
        if (MapScenes.Count == 0)
            return true;

        foreach (SceneReference scene in MapScenes)
            if (PlayerPrefs.HasKey("Visited" + scene.ScenePath))
                return true;
        
        return false;
    }

    public void SetupChangers()
    {
        Changers = new List<MapChanger>();
        foreach (Transform child in MapImageTransform)
            if (child.gameObject.TryGetComponent<MapChanger>(out MapChanger changer))
                Changers.Add(changer);
    }

    public void SetupMarkers()
    {
        _markers = new List<IMarker>();
        foreach (GameObject obj in _markerObjects)
            if (obj.TryGetComponent<IMarker>(out IMarker marker))
                _markers.Add(marker);
    }

    public virtual void DisplayPlayerMarker() { }

    private void OnEnable()
    {
        foreach (MapChanger changer in Changers)
            changer.DisplayMarkers();

        if (GetMarker<PlayerMarker>().ContainsInMap(this))       
            DisplayPlayerMarker();
    }

    private IEnumerator InitCoroutine()
    {
        yield return new WaitForEndOfFrame();
        OnEnable();
    }

    private void Awake()
    {
        SetupMarkers();
        SetupChangers();
    }
}
