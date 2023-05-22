using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.Interactable
{
    public sealed class NPCInteractableView : SerializedMonoBehaviour, INPCInteractableView
    {
        [SerializeField] private IUIActivityChanger _uiActivityChanger;

        public void DisplayInteraction()
            => _uiActivityChanger.TurnOff();

        public void DisplayEndOfInteraction()
            => _uiActivityChanger.TurnOn();
    }
}