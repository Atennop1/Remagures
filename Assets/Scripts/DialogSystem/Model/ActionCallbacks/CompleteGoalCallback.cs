using Remagures.QuestSystem;
using Remagures.SO.QuestSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class CompleteGoalCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private QuestGoal _goal;
        [FormerlySerializedAs("_questsDatabase")] [SerializeField] private QuestContainerOperations questContainerOperations;

        public void Callback() => questContainerOperations.TryCompleteGoal(_goal);
    }
}