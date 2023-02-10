using UnityEngine;

namespace Remagures.View.Character
{
    public class CharacterInteractorView : MonoBehaviour, ICharacterInteractorView
    {
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        
        public void DisplayEndOfInteraction()
        {
            _dialogWindow.SetActive(false);
            _uiActivityChanger.TurnOn();
        }
    }
}