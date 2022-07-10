using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsView : MonoBehaviour
{
    [SerializeField] private QuestContainer _playerQuests;
    [SerializeField] private QuestSlot _slotPrefab;
    [SerializeField] private GameObject _noQuestsText;

    [Space]
    [SerializeField] private GameObject _questsPanel;
    [SerializeField] private QuestGoalsView _goalsView;

    public void Start()
    {
        Initialize();
        Close();
    }

    public void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        ClearInventory();

        if (_playerQuests.Quests.Count == 0)
        {
            _noQuestsText.SetActive(true);
            return;
        }

        for (int i = 0; i < _playerQuests.Quests.Count; i++)
        {
            QuestSlot slot = Instantiate(_slotPrefab, _questsPanel.transform.position, Quaternion.identity, _questsPanel.transform);
            slot.Initialize(_playerQuests.Quests[i], _goalsView);
        }

    }

    private void ClearInventory()
    {
        _noQuestsText.SetActive(false);
        for (int i = 0; i < _questsPanel.transform.childCount; i++)
            Destroy(_questsPanel.transform.GetChild(i).gameObject);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
