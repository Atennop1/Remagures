using System;
using System.Linq;
using JetBrains.Annotations;
using Remagures.Model.QuestSystem;
using Remagures.Tools;
using Remagures.View.MapSystem;

namespace Remagures.Model.MapSystem
{
    public sealed class GoalMarker : IMarker
    {
        private readonly IMarkerView _markerView;
        private readonly IQuestsList _questsList;
        private readonly IQuest _quest;
        private readonly IQuestGoal _goal;

        public GoalMarker(IMarkerView markerView, IQuestsList questsList, IQuest quest, IQuestGoal goal)
        {
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
            _goal = goal ?? throw new ArgumentNullException(nameof(goal));
        }

        public bool IsActive() 
            => _questsList.Quests.Contains(_quest) && _goal == _quest.GetActiveGoal();

        public void OnEnable()
        {
            _markerView.UnDisplay();
            
            if (IsActive())
                _markerView.Display();
        }
    }
}
