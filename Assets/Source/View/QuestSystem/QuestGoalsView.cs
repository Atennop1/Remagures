using Remagures.Factories;
using Remagures.Model.QuestSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.QuestSystem
{
    public sealed class QuestGoalsView : MonoBehaviour, IQuestGoalsView
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _questImage;

        [SerializeField] private IGoalTextFactory _goalTextFactory;
        [SerializeField] private Transform _goalsContent;

        public void Display(IQuest quest)
        {
            gameObject.SetActive(true);
            _nameText.text = quest.Data.Name;
            _descriptionText.text = quest.Data.Description;
            _questImage.sprite = quest.Data.Sprite.Get();

            ClearContent();
            foreach (var goal in quest.Goals)
            {
                var goalText = _goalTextFactory.Create(_goalsContent);
                goalText.text = "* " + goal.Description;

                if (!goal.IsCompleted)
                    return;

                goalText.fontStyle = FontStyles.Strikethrough;
            }
        }

        private void ClearContent()
        {
            for (var i = 0; i < _goalsContent.childCount; i++)
                Destroy(_goalsContent.GetChild(i).gameObject);
        }

        private void Close()
            => gameObject.SetActive(false);
    }
}
