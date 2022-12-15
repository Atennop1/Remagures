using Remagures.QuestSystem;
using Remagures.SO.QuestSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.Dialogs.Model.ActionCallbacks
{
    public class AddQuestCallback : SerializedMonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private Quest _quest;
        [FormerlySerializedAs("_questsDatabase")] [SerializeField] private QuestContainerOperations questContainerOperations;

        public void Callback() => questContainerOperations.TryAddQuest(_quest);
    }
}