using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    [field: SerializeField] public List<SceneReference> MapScenes { get; private set; }
    [field: SerializeField, Space] public Map ParentMap { get; private set; }
    [field: SerializeField] protected RectTransform PlayerMarker { get; private set; }

    private List<MapChanger> _changers;

    public void Awake()
    {
        SetupChangers();
    }

    public void SetupChangers()
    {
        _changers = new List<MapChanger>();
        foreach (Transform child in PlayerMarker.parent)
            if (child.gameObject.TryGetComponent<MapChanger>(out MapChanger changer))
                _changers.Add(changer);
    }

    public void Init(MapView view)
    {
        foreach (MapChanger changer in _changers)
            changer.Init(view);
            
        StartCoroutine(InitCoroutine());
    }

    public bool ContainsPlayerInMapTree()
    {
        SetupChangers();
        if (ContainsPlayerOnMap())
            return true;
        
        foreach (MapChanger changer in _changers)
            if (changer.MapToOpen.ContainsPlayerInMapTree())
                return true;

        return false;
    }

    public bool ContainsPlayerOnMap()
    {
        foreach (SceneReference scene in MapScenes)
            if (scene.ScenePath == SceneManager.GetActiveScene().path)
                return true;

        return false;
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


    public virtual void DisplayPlayerMarker() { }

    private void OnEnable()
    {
        PlayerMarker.gameObject.SetActive(false);
        
        if (ContainsPlayerOnMap())       
            DisplayPlayerMarker();

        foreach (MapChanger changer in _changers)
            changer.DisplayPlayerMarker();
    }

    private IEnumerator InitCoroutine()
    {
        yield return new WaitForEndOfFrame();
        OnEnable();
    }
}
