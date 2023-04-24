using System.Collections.Generic;
using Remagures.Factories;
using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.View.QuestSystem
{
    public class QuestsView : MonoBehaviour
    {
        [SerializeField] private IQuestSlotViewFactory _questSlotViewFactory;
        [SerializeField] private GameObject _absenceQuestsText;

        [Space]
        [SerializeField] private Transform _questsContent;
        [SerializeField] private QuestGoalsView _goalsView;

        private readonly List<QuestSlotView> _spawnedQuestSlots = new();

        private void Display(IQuestsList questsList)
        {            
            ClearContent();
            _absenceQuestsText.SetActive(true);

            foreach (var quest in questsList.Quests)
            {
                _spawnedQuestSlots.Clear();
                var questSlot = _questSlotViewFactory.Create(_questsContent);
                questSlot.Display(quest);
                
                _spawnedQuestSlots.Add(questSlot);
                questSlot.Button.onClick.AddListener(() => _goalsView.Display(quest));
                _absenceQuestsText.SetActive(false);
            }
        }

        private void OnDestroy()
            => _spawnedQuestSlots.ForEach(questSlot => questSlot.Button.onClick.RemoveAllListeners());

        private void ClearContent()
        {
            for (var i = 0; i < _questsContent.childCount; i++)
                Destroy(_questsContent.GetChild(i).gameObject);
        }

        private void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
