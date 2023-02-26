using System;
using Remagures.Model.QuestSystem;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class AddQuestCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private QuestContainerOperations questContainerOperations;

        private IUsableComponent _usableComponent;
        private bool _isWorked;

        private void Update()
        {
            if (!_usableComponent.IsUsed || _isWorked)
                return;
            
            questContainerOperations.TryAddQuest(_quest);
            _isWorked = true;
        }

        public void Init(IUsableComponent usableComponent)
            => _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
    }
}