using System;
using Remagures.Model.QuestSystem;
using Remagures.Root;

namespace Remagures.Model.DialogSystem
{
    public sealed class AddQuestCallback : IUpdatable
    {
        private readonly IUsableComponent _usableComponent;
        private readonly IQuestsList _questsList;
        private readonly IQuest _quest;
        
        private bool _hasWorked;

        public AddQuestCallback(IUsableComponent usableComponent, IQuestsList questsList, IQuest quest)
        {
            _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
        }

        public void Update()
        {
            if (!_usableComponent.IsUsed || _hasWorked || !_questsList.CanAddQuest(_quest))
                return;
            
            _questsList.AddQuest(_quest);
            _hasWorked = true;
        }
    }
}