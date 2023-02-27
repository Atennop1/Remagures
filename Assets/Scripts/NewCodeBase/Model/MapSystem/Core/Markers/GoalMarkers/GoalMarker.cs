using System.Linq;
using Remagures.Model.QuestSystem;
using Remagures.SO;
using Remagures.View.QuestSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Remagures.Model.MapSystem
{
    public class GoalMarker : MonoBehaviour, IMarker //TODO remake this after quest system
    {
        [SerializeField] private QuestsList _questsList;
        [SerializeField] private QuestGoal _goal;
        [SerializeField] private Image _marker;

        private QuestGoalsView _goalsView;

        public void Init(QuestGoalsView view)
            => _goalsView = view;

        public void OpenGoals()
        {
            _goalsView.gameObject.SetActive(true);
            _goalsView.Display(_goal.Quest);
        }

        public bool IsActive()
        {
            var quest = _goal.Quest;
            return _questsList.Quests.Contains(quest) && _goal == quest.GetActiveGoal();
        }

        private void OnEnable()
        {
            _marker.gameObject.SetActive(false);
            if (_goal.IsCompleted) return;
        
            if (IsActive())
                _marker.gameObject.SetActive(true);
        }
    }
}
