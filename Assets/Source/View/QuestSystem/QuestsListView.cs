using System;
using System.Collections.Generic;
using Remagures.Factories;
using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.View.QuestSystem
{
    public sealed class QuestsListView : MonoBehaviour
    {
        [SerializeField] private IQuestSlotViewFactory _questSlotViewFactory;
        [SerializeField] private GameObject _absenceQuestsText;

        [Space]
        [SerializeField] private Transform _questsContent;
        [SerializeField] private IQuestGoalsView _goalsView;

        private readonly List<IQuestSlotView> _spawnedQuestSlots = new();
        private IQuestsList _questsList;

        public void Construct(IQuestsList questsList)
            => _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));

        private void OnDestroy()
            => ClearContent();

        private void OnEnable() 
            => Display();

        private void Display()
        {            
            ClearContent();
            _absenceQuestsText.SetActive(true);

            foreach (var quest in _questsList.Quests)
            {
                var questSlot = _questSlotViewFactory.Create(_questsContent);
                questSlot.Display(quest);
                
                _spawnedQuestSlots.Add(questSlot);
                questSlot.Button.onClick.AddListener(() => _goalsView.Display(quest));
                _absenceQuestsText.SetActive(false);
            }
        }

        private void ClearContent()
        {
            _spawnedQuestSlots.ForEach(questSlot => questSlot.Button.onClick.RemoveAllListeners());
            _spawnedQuestSlots.Clear();
            
            for (var i = 0; i < _questsContent.childCount; i++)
                Destroy(_questsContent.GetChild(i).gameObject);
        }
    }
}
