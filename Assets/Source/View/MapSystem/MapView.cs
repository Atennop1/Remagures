using Remagures.Factories;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.MapSystem
{
    public sealed class MapView : SerializedMonoBehaviour, IMapView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private IMapObjectFactory _mapObjectFactory;
        [SerializeField] private Animator _animator;
        
        public void Display(IMap map)
        {
            ClearMap();
            _mapObjectFactory.Create(map, _content);
        }

        public void DisplayFailure()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                _animator.Play("Cant");
        }

        private void ClearMap()
        {
            foreach (Transform child in _content)
                Destroy(child.gameObject);
        }
    }
}
