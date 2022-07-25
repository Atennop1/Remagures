using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapView : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [Space]
    [SerializeField] private Transform _mapContainer;
    [SerializeField] private Button _changeScaleButton;

    [Space]
    [SerializeField] private Animator _animator;

    private Map _currentMap;

    public void OpenParentMap()
    {
        if (_currentMap.ParentMap.IsVisited())
            OpenMap(_currentMap.ParentMap);
        else
            CantOpenMap();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenMap(Map map)
    {
        ClearMap();

        _currentMap = Instantiate(map, transform.position, Quaternion.identity, _mapContainer);
        (_currentMap as LocationMap)?.Init(_player, this);
        (_currentMap as Map)?.Init(this);

        _changeScaleButton.interactable = _currentMap.ParentMap != null;
    }

    public void CantOpenMap()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            _animator.Play("Cant");
    }

    private void ClearMap()
    {
        foreach (Transform child in _mapContainer)
            Destroy(child.gameObject);
    }
}
