using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.MapSystem
{
    public sealed class CharacterMarkerView : SerializedMonoBehaviour, ICharacterMarkerView
    {
        [SerializeField] private GameObject _markerGameObject;

        public void Display(Vector2 position)
        {
            _markerGameObject.SetActive(true);
            _markerGameObject.transform.position = position;
        }

        public void UnDisplay()
            => _markerGameObject.SetActive(false);
    }
}