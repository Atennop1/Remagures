using Remagures.MapSystem.Maps;
using Remagures.Quest_System;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.MapSystem
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private QuestGoalsView _goalsView;

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
            UnityEngine.Time.timeScale = 1;
        }

        public void OpenMap(Map map)
        {
            ClearMap();

            _currentMap = Instantiate(map, transform.position, Quaternion.identity, _mapContainer);
            _currentMap.Init(this, _goalsView);
            (_currentMap as LocationMap)?.Init(this, _goalsView, _player);

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

        private void Start()
        {
            Close();
        }
    }
}
