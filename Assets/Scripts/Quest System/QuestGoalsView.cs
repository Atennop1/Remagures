using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        ClearInventory();
        for (int i = 0; i < quest.Goals.Count; i++)
        {
            TextMeshProUGUI description = Instantiate(_textPrefab, _goalsPanel.transform.position, Quaternion.identity, _goalsPanel.transform);
            description.text = "* " + quest.Goals[i].Description;

            if (!quest.Goals[i].Completed)
                return;

            description.fontStyle = FontStyles.Strikethrough;
        }
    }

    private void ClearInventory()
    {
        for (int i = 0; i < _goalsPanel.transform.childCount; i++)
            Destroy(_goalsPanel.transform.GetChild(i).gameObject);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
