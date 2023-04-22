using System;
using Remagures.Model.InventorySystem;
using Remagures.Model.QuestSystem;
using Remagures.Tools;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithGoalCompleting : IChest
    {
        public bool HasInteractionEnded => _chest.HasInteractionEnded;
        public bool IsOpened => _chest.IsOpened;
        public IItem Item => _chest.Item;

        private readonly IChest _chest;
        private readonly IQuestGoal _goal;

        public ChestWithGoalCompleting(IChest chest, IQuestGoal goal)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _goal = goal ?? throw new ArgumentNullException(nameof(goal));
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _goal.Complete();
        }

        public void OnInteractionEnd()
            => _chest.OnInteractionEnd();
    }
}