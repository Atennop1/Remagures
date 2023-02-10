using System;
using Remagures.SO;
using Remagures.View.Interactable;

namespace Remagures.Model.Interactable
{
    public class ChestWithItemRaising : IChest
    {
        public bool HasInteracted => _chest.HasInteracted;
        public bool IsOpened => _chest.IsOpened;
        public BaseInventoryItem Item => _chest.Item;

        private readonly IChest _chest;
        private readonly IChestWithItemRaisingView _view;

        public ChestWithItemRaising(IChest chest, IChestWithItemRaisingView view)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _view.Display(Item);
        }

        public void EndInteracting()
            => _view.EndDisplaying();
    }
}