using System;
using Remagures.QuestSystem;
using Remagures.SO;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithGoalCompleting : IChest
    {
        public bool HasInteracted => _chest.HasInteracted;
        public bool IsOpened => _chest.IsOpened;
        public BaseInventoryItem Item => _chest.Item;

        private readonly IChest _chest;
        private readonly GoalCompleter _goalCompleter;

        public ChestWithGoalCompleting(IChest chest, GoalCompleter goalCompleter)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _goalCompleter = goalCompleter ?? throw new ArgumentNullException(nameof(goalCompleter));
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _goalCompleter?.Complete();
        }

        public void EndInteracting()
            => _chest.EndInteracting();
    }
}