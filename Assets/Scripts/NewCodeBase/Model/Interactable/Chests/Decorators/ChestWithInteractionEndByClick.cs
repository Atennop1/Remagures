using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithInteractionEndByClick : IChest
    {
        public bool HasInteractionEnded => _interactableWithInteractionEndByClick.HasInteractionEnded;
        public bool IsOpened => _chest.IsOpened;
        public IItem Item => _chest.Item;

        private readonly IChest _chest;
        private readonly InteractableWithInteractionEndByClick _interactableWithInteractionEndByClick;

        public ChestWithInteractionEndByClick(IChest chest, InteractableWithInteractionEndByClick interactableWithInteractionEndByClick)
        {
            _interactableWithInteractionEndByClick = interactableWithInteractionEndByClick ?? throw new ArgumentNullException(nameof(interactableWithInteractionEndByClick));
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _interactableWithInteractionEndByClick.Interact();
            _chest.Interact();
        }

        public void EndInteracting()
        {
            _interactableWithInteractionEndByClick.EndInteracting();
            _chest.EndInteracting();
        }
    }
}