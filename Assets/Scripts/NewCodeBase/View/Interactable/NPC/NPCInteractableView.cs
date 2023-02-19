using UnityEngine;

namespace Remagures.View.Interactable
{
    public class NPCInteractableView : MonoBehaviour, INPCInteractableView
    {
        [SerializeField] private UIActivityChanger _uiActivityChanger;

        public void DisplayInteraction()
            => _uiActivityChanger.TurnOff();

        public void DisplayEndOfInteraction()
            => _uiActivityChanger.TurnOn();
    }
}