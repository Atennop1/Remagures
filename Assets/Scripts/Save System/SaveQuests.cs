using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuests : Saver, ISaver
{
    [Space]
    [SerializeField] private GameSaveContainer _saveContainer;
    [SerializeField] private QuestContainer _questsContainer;

    [Space]
    [SerializeField] private List<Quest> _quests;

    public void Start()
    {
        for (int i = 0; i < _quests.Count; i++)
            _quests[i].Initialize();
    }

    public void Save()
    {
        for (int i = 0; i < _questsContainer.Quests.Count; i++)
        {
            Quest currentQuest = _questsContainer.Quests[i];
            FloatValue value = ScriptableObject.CreateInstance<FloatValue>();

            value.Value = _quests.IndexOf(currentQuest);
            _saveContainer.CheckDir($"{Path}/Quest{i + 1}");
            SaveObjectToJson($"{Path}/Quest{i + 1}/ID.json", value);

            for (int j = 0; j < currentQuest.Goals.Count; j++)
            {
                value.Value = currentQuest.Goals[j].CurrentAmount;
                SaveObjectToJson($"{Path}/Quest{i + 1}/Goal{j + 1}Amount.json", value);
            }
        }
    }

    public void Load()
    {
        for (int i = 0; i < _questsContainer.Quests.Count; i++)
        {
            Quest currentQuest = _questsContainer.Quests[i];
            FloatValue value = ScriptableObject.CreateInstance<FloatValue>();

            _saveContainer.CheckDir($"{Path}/Quest{i + 1}");
            LoadObjectFromJson($"{Path}/Quest{i + 1}/ID.json", value);
            _questsContainer.TryAddQuest(_quests[(int)value.Value]);

            for (int j = 0; j < currentQuest.Goals.Count; j++)
            {
                LoadObjectFromJson($"{Path}/Quest{i + 1}/Goal{j + 1}Amount.json", value);
                currentQuest.Goals[j].Reset();

                for (int _ = 0; _ < value.Value; _++)
                    currentQuest.Goals[j].AddAmount();
            }
        }
    }

    public void NewGame()
    {
        for (int i = 0; i < _quests.Count; i++)
            for (int j = 0; j < _quests[i].Goals.Count; j++)
                _quests[i].Goals[j].Reset();

        _questsContainer.Reset();
    }
}
