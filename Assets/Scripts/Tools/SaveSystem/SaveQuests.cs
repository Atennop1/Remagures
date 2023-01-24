using System.Collections.Generic;
using System.Linq;
using Remagures.SaveSystem.Abstraction;
using Remagures.SO.PlayerStuff;
using Remagures.SO.QuestSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.SaveSystem
{
    public class SaveQuests : Saver, ISaver
    {
        [Space]
        [FormerlySerializedAs("_saveContainer")]
        [SerializeField] private GameSaver _gameSaver;
        [SerializeField] private QuestContainer _questsContainer;

        [Space]
        [SerializeField] private List<Quest> _quests;

        public void Start()
        {
            foreach (var quest in _quests)
                quest.Initialize();
        }

        public void Save()
        {
            for (var i = 0; i < _questsContainer.Quests.Count; i++)
            {
                var currentQuest = _questsContainer.Quests[i];
                var value = ScriptableObject.CreateInstance<FloatValue>();

                value.Value = _quests.IndexOf(currentQuest);
                _gameSaver.CheckDir($"{Path}/Quest{i + 1}");
                SaveObjectToJson($"{Path}/Quest{i + 1}/ID.json", value);

                for (var j = 0; j < currentQuest.Goals.Count; j++)
                {
                    value.Value = currentQuest.Goals[j].CurrentAmount;
                    SaveObjectToJson($"{Path}/Quest{i + 1}/Goal{j + 1}Amount.json", value);
                }
            }
        }

        public void Load()
        {
            for (var i = 0; i < _questsContainer.Quests.Count; i++)
            {
                var currentQuest = _questsContainer.Quests[i];
                var value = ScriptableObject.CreateInstance<FloatValue>();

                _gameSaver.CheckDir($"{Path}/Quest{i + 1}");
                LoadObjectFromJson($"{Path}/Quest{i + 1}/ID.json", value);
                _questsContainer.TryAddQuest(_quests[(int)value.Value]);

                for (var j = 0; j < currentQuest.Goals.Count; j++)
                {
                    LoadObjectFromJson($"{Path}/Quest{i + 1}/Goal{j + 1}Amount.json", value);
                    currentQuest.Goals[j].ResetCurrentAmount();

                    for (var _ = 0; _ < value.Value; _++)
                        currentQuest.Goals[j].AddAmount();
                }
            }
        }

        public void NewGame()
        {
            foreach (var goal in _quests.SelectMany(quest => quest.Goals))
            {
                goal.Reset();
                goal.Quest.Reset();
            }

            _questsContainer.Reset();
        }
    }
}
