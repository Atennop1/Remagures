using System;
using Remagures.QuestSystem;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class CompleteGoalCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private QuestGoal _goal;
        [SerializeField] private QuestContainerOperations questContainerOperations;

        private IUsableComponent _usableComponent;
        private bool _isWorked;

        private void Update()
        {
            if (!_usableComponent.IsUsed || _isWorked) 
                return;
            
            questContainerOperations.TryCompleteGoal(_goal);
            _isWorked = true;
        }

        public void Init(IUsableComponent usableComponent)
            => _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
    }
}