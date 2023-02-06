using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Tools
{
    public class SaveStats : Saver, ISaver
    {
        [SerializeField] private List<ScriptableObject> _stats;
        [SerializeField] private List<ScriptableObject> _defaultStats;

        public void Save()
        {
            for (var i = 0; i < _stats.Count; i++)
                SaveObjectToJson(Path + "/StatSave" + (i + 1) + ".json", _stats[i]);
        }

        public void Load()
        {
            for (var i = 0; i < _stats.Count; i++)
                LoadObjectFromJson(Path + "/StatSave" + (i + 1) + ".json", _stats[i]);
        }
    
        public void NewGame()
        {
            for (var i = 0; i < _stats.Count; i++)
                _stats[i] = _defaultStats[i];
        }
    }
}
