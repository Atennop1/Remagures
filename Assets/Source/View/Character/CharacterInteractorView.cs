using UnityEngine;

namespace Remagures.View.Character
{
    public sealed class CharacterInteractorView : MonoBehaviour, ICharacterInteractorView
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