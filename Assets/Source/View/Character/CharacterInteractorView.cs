using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.Character
{
    public sealed class CharacterInteractorView : SerializedMonoBehaviour, ICharacterInteractorView
    {
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private IUIActivityChanger _uiActivityChanger;
        
        public void DisplayEndOfInteraction()
        {
            _dialogWindow.SetActive(false);
            _uiActivityChanger.TurnOn();
        }
    }
}