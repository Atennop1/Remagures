using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Image _questImage;

    private Quest _thisQuest;
    private QuestGoalsView _goalsPanel;

    public void Initialize(Quest quest, QuestGoalsView goalsPanel)
    {
        _nameText.text = quest.Information.Name;
        _descriptionText.text = quest.Information.Description;
        _questImage.sprite = quest.Information.QuestSprite;

        _thisQuest = quest;
        _goalsPanel = goalsPanel;
    }

    public void ShowGoals()
    {
        _goalsPanel.gameObject.SetActive(true);
        _goalsPanel.Initialize(_thisQuest);
    }
}
