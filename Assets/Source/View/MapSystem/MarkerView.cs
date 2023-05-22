using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.MapSystem
{
    public sealed class MarkerView : SerializedMonoBehaviour, IMarkerView
    {
        [SerializeField] private GameObject _markerGameObject;
        
        public void Display()
            => _markerGameObject.SetActive(true);

        public void UnDisplay()
            => _markerGameObject.SetActive(false);
    }
}