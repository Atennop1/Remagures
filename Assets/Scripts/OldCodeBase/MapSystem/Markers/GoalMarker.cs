using System.Linq;
using Remagures.QuestSystem;
using Remagures.SO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Remagures.MapSystem
{
    public class GoalMarker : MonoBehaviour
    {
        [SerializeField] private QuestContainer _questContainer;
        [FormerlySerializedAs("Goal")] [SerializeField] private QuestGoal _goal;
        [SerializeField] private Image _marker;

        private QuestGoalsView _goalsView;

        public void Init(QuestGoalsView view)
            => _goalsView = view;

        public void OpenGoals()
        {
            _goalsView.gameObject.SetActive(true);
            _goalsView.Initialize(_goal.Quest);
        }

        public bool IsGoalActive()
        {
            var quest = _goal.Quest;
            return _questContainer.Quests.Contains(quest) && _goal == quest.GetActiveGoal();
        }

        private void OnEnable()
        {
            _marker.gameObject.SetActive(false);
            if (_goal.Completed) return;
        
            if (IsGoalActive())
                _marker.gameObject.SetActive(true);
        }
    }
}
