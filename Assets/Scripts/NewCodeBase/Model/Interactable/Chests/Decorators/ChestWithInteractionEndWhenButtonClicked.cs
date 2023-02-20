using System;
using Remagures.Model.InventorySystem;
using Remagures.SO;
using UnityEngine.UI;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithInteractionEndWhenButtonClicked : IChest
    {
        public bool HasInteractionEnded { get; private set; }
        public bool IsOpened => _chest.IsOpened;
        public Item Item => _chest.Item;

        private readonly IChest _chest;
        private readonly Button _endButton;

        public ChestWithInteractionEndWhenButtonClicked(IChest chest, Button endButton)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _endButton = endButton ?? throw new ArgumentNullException(nameof(endButton));
            
            _endButton.onClick.AddListener(OnClick);
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
        }

        public void EndInteracting()
        {
            _endButton.onClick.RemoveListener(OnClick);
            _chest.EndInteracting();
        }

        private void OnClick()
            => HasInteractionEnded = true;
    }
}