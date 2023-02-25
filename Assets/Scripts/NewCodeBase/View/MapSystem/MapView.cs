using Remagures.Factories;
using UnityEngine;

namespace Remagures.View.MapSystem
{
    public sealed class MapView : MonoBehaviour, IMapView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private IMapsFactory _mapsFactory;
        [SerializeField] private Animator _animator;
        
        public void Display()
        {
            ClearMap();
            _mapsFactory.Create(_content);
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
