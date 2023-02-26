using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.View.QuestSystem
{
    public class QuestsView : MonoBehaviour
    {
        [SerializeField] private QuestsList _playerQuests;
        [SerializeField] private QuestSlotView slotViewPrefab;
        [SerializeField] private GameObject _noQuestsText;

        [Space]
        [SerializeField] private GameObject _questsPanel;
        [SerializeField] private QuestGoalsView _goalsView;

        private void Start()
        {
            Init();
            Close();
        }

        private void OnEnable()
            => Init();

        private void Init()
        {
            ClearInventory();

            if (_playerQuests.Quests.Count == 0)
            {
                _noQuestsText.SetActive(true);
                return;
            }

            foreach (var quest in _playerQuests.Quests)
            {
                var slot = Instantiate(slotViewPrefab, _questsPanel.transform.position, Quaternion.identity, _questsPanel.transform);
                slot.Initialize(quest, _goalsView);
            }
        }

        private void ClearInventory()
        {
            _noQuestsText.SetActive(false);
            for (var i = 0; i < _questsPanel.transform.childCount; i++)
                Destroy(_questsPanel.transform.GetChild(i).gameObject);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            UnityEngine.Time.timeScale = 1;
        }
    }
}
