using Remagures.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.QuestSystem
{
    public class QuestGoalsView : MonoBehaviour
    {
        [SerializeField] public Text _nameText;
        [SerializeField] public Text _descriptionText;
        [SerializeField] public Image _questImage;

        [Space]
        [SerializeField] public TextMeshProUGUI _textPrefab;
        [SerializeField] public GameObject _goalsPanel;

        public void Initialize(Quest quest)
        {
            _nameText.text = quest.Information.Name;
            _descriptionText.text = quest.Information.Description;
            _questImage.sprite = quest.Information.QuestSprite;

            Clear();
            foreach (var goal in quest.Goals)
            {
                var description = Instantiate(_textPrefab, _goalsPanel.transform.position, Quaternion.identity, _goalsPanel.transform);
                description.text = "* " + goal.Description;

                if (!goal.Completed)
                    return;

                description.fontStyle = FontStyles.Strikethrough;
            }
        }

        private void Clear()
        {
            for (var i = 0; i < _goalsPanel.transform.childCount; i++)
                Destroy(_goalsPanel.transform.GetChild(i).gameObject);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
