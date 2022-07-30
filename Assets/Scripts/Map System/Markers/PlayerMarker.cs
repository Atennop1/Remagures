using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMarker : MonoBehaviour, IMarker
{
    [SerializeField] private Transform _playerMarker;
    public IMarkerVisitor Visitor { get; private set; }

    public void Init(MapView view) { }

    public void SetupComponents()
    { 
        Visitor = new PlayerMarkerVisitor();
    }

    public bool ContainsInMap(Map map)
    {
        foreach (SceneReference scene in map.MapScenes)
            if (scene.ScenePath == SceneManager.GetActiveScene().path)
                return true;

        return false;
    }

    public bool ContainsInMapTree(Map map)
    {
        map.SetupChangers();
        if (ContainsInMap(map))
            return true;
        
        foreach (MapChanger changer in map.Changers)
            if (changer.ContainsInMapTree<PlayerMarker>())
                return true;

        return false;
    }

    public bool TryDisplay(Transform parent, Vector3 scale, Map map)
    {
        _playerMarker.gameObject.SetActive(false);
        if (ContainsInMapTree(map))
        {
            _playerMarker.localScale = scale;

            _playerMarker.gameObject.SetActive(true);
            _playerMarker.SetParent(parent);

            map.ParentMap?.SetupMarkers();
            foreach (IMarker marker in map.ParentMap?.Markers)
            {
                marker.SetupComponents();
                marker.Visitor.Visit(this);
            }
            
            return true;
        }

        return false;
    }

    private class PlayerMarkerVisitor : IMarkerVisitor
    {
        public void Visit(IMarker marker) { }
    }
}
