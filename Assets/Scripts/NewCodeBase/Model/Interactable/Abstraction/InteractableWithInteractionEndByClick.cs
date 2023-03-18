using System;
using UnityEngine.UI;

namespace Remagures.Model.Interactable
{
    public sealed class InteractableWithInteractionEndByClick : IInteractable
    {
        public bool HasInteractionEnded { get; private set; }
        private readonly Button _endButton;

        public InteractableWithInteractionEndByClick(Button endButton)
        {
            _endButton = endButton ?? throw new ArgumentNullException(nameof(endButton));
            _endButton.onClick.AddListener(OnClick);
        }

        public void EndInteracting()
            => _endButton.onClick.RemoveListener(OnClick);
        

        private void OnClick()
            => HasInteractionEnded = true;
        
        public void Interact() { }
    }
}