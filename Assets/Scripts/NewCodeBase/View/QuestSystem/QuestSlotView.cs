using Remagures.Model.QuestSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.QuestSystem
{
    public class QuestSlotView : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _questImage;

        private Quest _thisQuest;
        private QuestGoalsView _goalsPanel;

        public void Initialize(Quest quest, QuestGoalsView goalsPanel)
        {
            _nameText.text = quest.Data.Name;
            _descriptionText.text = quest.Data.Description;
            _questImage.sprite = quest.Data.QuestSprite;

            _thisQuest = quest;
            _goalsPanel = goalsPanel;
        }

        public void ShowGoals()
        {
            _goalsPanel.gameObject.SetActive(true);
            _goalsPanel.Initialize(_thisQuest);
        }
    }
}
