using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.QuestSystem;
using Remagures.SO.QuestSystem;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class AddQuestLineEndCallback : MonoBehaviour, ILineEndCallback
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private QuestContainerOperations questContainerOperations;
        
        private DialogLine _dialogLine;
        private bool _isWorked;
        
        private void Update()
        {
            if (!_dialogLine.IsEnded || _isWorked)
                return;
            
            questContainerOperations.TryAddQuest(_quest);
            _isWorked = true;
        }
        
        public void Init(DialogLine line)
            => _dialogLine = line ?? throw new ArgumentNullException(nameof(line));

    }
}