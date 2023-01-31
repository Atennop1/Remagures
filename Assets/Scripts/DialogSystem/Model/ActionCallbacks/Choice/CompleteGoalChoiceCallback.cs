using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.QuestSystem;
using Remagures.SO.QuestSystem;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks.Choice
{
    public class CompleteGoalChoiceCallback : MonoBehaviour, IChoiceCallback
    {
        [SerializeField] private QuestGoal _goal;
        [SerializeField] private QuestContainerOperations questContainerOperations;

        private DialogChoice _dialogChoice;
        private bool _isWorked;

        private void Update()
        {
            if (!_dialogChoice.IsUsed || _isWorked) 
                return;
            
            questContainerOperations.TryCompleteGoal(_goal);
            _isWorked = true;
        }

        public void Init(DialogChoice choice)
            => _dialogChoice = choice ?? throw new ArgumentNullException(nameof(choice));
    }
}